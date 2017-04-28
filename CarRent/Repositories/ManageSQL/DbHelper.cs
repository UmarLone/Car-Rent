
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Repositories.ManageSQL
{
    /// <summary>
    /// Base class for DbHelpers, with strongly typed Connection, Command, and ParameterCollection
    /// clases.
    /// Strong typing allows us to take advantage of convenience methods like SqlParameterCollection.AddWithValue
    /// </summary>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TParameterCollection"></typeparam>
    public abstract class DbHelper<TConnection, TCommand, TParameterCollection>
        where TConnection : class, IDbConnection
        where TCommand : class, IDbCommand
        where TParameterCollection : class, IDataParameterCollection
    {
        public string ConnectionString { get; protected set; }
        protected TConnection SharedConnection { get; set; }

        protected abstract TCommand CreateCommand(string cmdText, TConnection connection);
        protected abstract TConnection CreateConnection(string connectionString);
        protected abstract IDbDataAdapter CreateDataAdapter(TCommand cmd);

        public DbHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Open a shared connection for use in transactions
        /// </summary>
        /// <returns></returns>
        public virtual IDisposable BeginSharedConnection()
        {
            if (this.SharedConnection != null)
            {
                throw new InvalidOperationException("Shared connection already in progress!");
            }

            this.SharedConnection = CreateConnection(this.ConnectionString);
            this.SharedConnection.Open();

            return new DisposeHelper(() =>
            {
                this.SharedConnection.Dispose();
                this.SharedConnection = null;
            });
        }

        // Execute command with no parameters, and no return
        public void ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            ExecuteNonQuery(cmdType, cmdText, null);
        }
        // Execute command with parameters, and no return
        public void ExecuteNonQuery(CommandType cmdType, string cmdText, Action<TParameterCollection> paramAction)
        {
            ExecuteNonQuery(cmdType, cmdText, paramAction, c => true);
        }
        // Execute command with parameters, and a return value (from an output SqlParameter)
        public T ExecuteNonQuery<T>(CommandType cmdType, string cmdText, Action<TParameterCollection> paramAction, Func<TCommand, T> returnValue)
        {
            return ExecuteNonQuery(cmdType, cmdText, null, paramAction, returnValue);
        }
        // Execute command with parameters, and a return value (from an output SqlParameter).
        // Also gives full access to the SqlCommand object to set other properties, eg., timeouts
        public virtual T ExecuteNonQuery<T>(CommandType cmdType, string cmdText, Action<TCommand> cmdAction, Action<TParameterCollection> paramAction, Func<TCommand, T> returnValue)
        {
            if (returnValue == null) throw new ArgumentNullException("returnValue");

            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            try
            {
                using (var cmd = CreateCommand(cmdText, conn))
                {
                    cmd.CommandType = cmdType;
                    if (cmdAction != null) cmdAction(cmd);
                    if (paramAction != null) paramAction((TParameterCollection)cmd.Parameters);

                    if (this.SharedConnection != conn) conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return returnValue(cmd);
                    }
                    finally
                    {
                        if (this.SharedConnection != conn) conn.Close();
                    }
                }
            }
            finally
            {
                if (this.SharedConnection != conn) conn.Dispose();
            }
        }

        // Execute a scalar command, with parameters (or null if no parameters).
        // convertValue must handle converting from DBNull.Value
        public virtual T ExecuteScalar<T>(CommandType cmdType, string cmdText, Action<TParameterCollection> paramAction, Func<Object, T> convertValue)
        {
            if (convertValue == null) throw new ArgumentNullException("convertValue");

            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            try
            {
                using (var cmd = CreateCommand(cmdText, conn))
                {
                    cmd.CommandType = cmdType;
                    if (paramAction != null) paramAction((TParameterCollection)cmd.Parameters);

                    if (this.SharedConnection != conn) conn.Open();
                    try
                    {
                        return convertValue(cmd.ExecuteScalar());
                    }
                    finally
                    {
                        if (this.SharedConnection != conn) conn.Close();
                    }
                }
            }
            finally
            {
                if (this.SharedConnection != conn) conn.Dispose();
            }
        }

        // Execute a command returning a single row in resultset.
        // map Func gets access to IDataReader to map values from IDataReader to a strongly typed (or anonymous) object
        // If no rows are returned from resultset, returns default(T)
        public virtual T ExecuteSingle<T>(CommandType cmdType, string cmdText, Action<TParameterCollection> paramAction, Func<IDataReader, T> map)
        {
            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            try
            {
                using (var cmd = CreateCommand(cmdText, conn))
                {
                    cmd.CommandType = cmdType;
                    if (paramAction != null) paramAction((TParameterCollection)cmd.Parameters);

                    if (this.SharedConnection != conn) conn.Open();
                    try
                    {
                        using (var dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!dr.Read())
                            {
                                return default(T);
                            }
                            return map(dr);
                        }
                    }
                    finally
                    {
                        if (this.SharedConnection != conn) conn.Close();
                    }
                }
            }
            finally
            {
                if (this.SharedConnection != conn) conn.Dispose();
            }
        }

        // Execute a command returning multiple rows in resultset.
        // map Func gets access to IDataReader to map values from IDataReader to a strongly typed (or anonymous) object
        // If no rows are returned from resultset, returns empty set
        public virtual IEnumerable<T> ExecuteSet<T>(CommandType cmdType, string cmdText, Action<TParameterCollection> paramAction, Func<IDataReader, T> map)
        {
            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            try
            {
                using (var cmd = CreateCommand(cmdText, conn))
                {
                    cmd.CommandType = cmdType;
                    if (paramAction != null) paramAction((TParameterCollection)cmd.Parameters);

                    if (this.SharedConnection != conn) conn.Open();
                    try
                    {
                        using ( var dr = cmd.ExecuteReader(CommandBehavior.SingleResult))
                        {
                            while (dr.Read())
                            {
                                yield return map(dr);
                            }
                        }
                    }
                    finally
                    {
                        if (this.SharedConnection != conn) conn.Close();
                    }
                }
            }
            finally
            {
                if (this.SharedConnection != conn) conn.Dispose();
            }
        }

        public DataTable FillDataTable(CommandType cmdType, string cmdText)
        {
            return FillDataTable(cmdType, cmdText, null);
        }
        public DataTable FillDataTable(CommandType cmdType, string cmdText, Action<TParameterCollection> paramAction)
        {
            return FillDataSet(cmdType, cmdText, paramAction).Tables[0];
        }
        public DataSet FillDataSet(CommandType cmdType, string cmdText)
        {
            return FillDataSet(cmdType, cmdText, null);
        }
        public virtual DataSet FillDataSet(CommandType cmdType, string cmdText, Action<TParameterCollection> paramAction)
        {
            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            try
            {
                using (var cmd = CreateCommand(cmdText, conn))
                using (var da = (IDisposable)CreateDataAdapter(cmd))
                {
                    cmd.CommandType = cmdType;
                    if (paramAction != null) paramAction((TParameterCollection)cmd.Parameters);

                    var ds = new DataSet();
                    if (this.SharedConnection != conn) conn.Open();
                    try
                    {
                        ((IDbDataAdapter)da).Fill(ds);
                        return ds;
                    }
                    finally
                    {
                        if (this.SharedConnection != conn) conn.Close();
                    }
                }
            }
            finally
            {
                if (this.SharedConnection != conn) conn.Dispose();
            }
        }
    }
}
