using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics;
using Repositories.ManageSQL;

namespace Repositories.ManageSQL
{
    /// <summary>
    /// Strongly typed DBHelper for MS SQL Server
    /// </summary>
    public partial class SqlHelper : DbHelper<SqlConnection, SqlCommand, SqlParameterCollection>
    {
        public static string BuildTrustedConnectionString(string serverName, string databaseName)
        {
            var s = new SqlConnectionStringBuilder();
            s.DataSource = serverName;
            s.InitialCatalog = databaseName;
            s.IntegratedSecurity = true;
            return s.ToString();
        }
        public static string BuildConnectionString(string serverName, string databaseName, string userName, string password)
        {
            var s = new SqlConnectionStringBuilder();
            s.DataSource = serverName;
            s.InitialCatalog = databaseName;
            s.UserID = userName;
            s.Password = password;
            return s.ToString();
        }
        public static string GetConnectionString(string connectionStringKey)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
        }

        public int DefaultCommandTimeout { get; set; }

        public SqlHelper(string connectionString)
            : base(connectionString)
        {
            this.DefaultCommandTimeout = 30;
        }

        protected override SqlConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        protected override SqlCommand CreateCommand(string cmdText, SqlConnection connection)
        {
            return new SqlCommand(cmdText, connection) { CommandTimeout = DefaultCommandTimeout };
        }

        protected override IDbDataAdapter CreateDataAdapter(SqlCommand cmd)
        {
            return new SqlDataAdapter(cmd);
        }

        public SafeDataReader ExecuteSafeReader(CommandType cmdType, string cmdText, Action<SqlParameterCollection> paramAction)
        {
            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            var cmd = CreateCommand(cmdText, conn);
            cmd.CommandType = cmdType;
            if (paramAction != null) paramAction(cmd.Parameters);

            if (this.SharedConnection != conn) conn.Open();

            var dr = cmd.ExecuteReader();

            var d = new DisposeHelper(() =>
            {
                dr.Dispose();
                cmd.Dispose();
                if (this.SharedConnection != conn)
                {
                    conn.Dispose();
                }
            });
            return new SafeDataReader(dr, d);
        }
        // Use a SqlDataReader to stream results
        protected SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, Action<SqlParameterCollection> paramAction)
        {
            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            var cmd = CreateCommand(cmdText, conn);
            cmd.CommandType = cmdType;
            if (paramAction != null) paramAction(cmd.Parameters);

            if (this.SharedConnection != conn) conn.Open();
            return cmd.ExecuteReader(); // WARNING: Leaks SqlCommand and SqlConnection IDisposables
        }

        // Bulk copy implementations
        public void BulkCopy(SqlDataReader dr, string destinationTable, int batchSize, ColumnMatchingMode mode)
        {
            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            try
            {
                using (var bc = new SqlBulkCopy(conn))
                {
                    bc.DestinationTableName = destinationTable;
                    bc.BatchSize = batchSize;
                    bc.NotifyAfter = batchSize;

                    switch (mode)
                    {
                        case ColumnMatchingMode.Ordinal:
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                bc.ColumnMappings.Add(i, i);
                            }
                            break;
                        case ColumnMatchingMode.CaseSensitiveName:
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                string columnName = dr.GetName(i);
                                bc.ColumnMappings.Add(columnName, columnName);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("mode");
                    }

                    int totalRows = 0;
                    bc.SqlRowsCopied += (s, e) =>
                    {
                        Trace.TraceInformation("{0} rows copied...", totalRows += batchSize);
                    };

                    if (this.SharedConnection != conn) conn.Open();
                    try
                    {
                        bc.WriteToServer(dr);
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
        public void BulkCopy(DataTable dt, string destinationTable)
        {
            BulkCopy(dt, destinationTable, ColumnMatchingMode.Ordinal);
        }
        public void BulkCopy(DataTable dt, string destinationTable, ColumnMatchingMode mode)
        {
            var c = new List<SqlBulkCopyColumnMapping>(dt.Columns.Count);
            switch (mode)
            {
                case ColumnMatchingMode.Ordinal:
                    foreach (DataColumn dc in dt.Columns)
                    {
                        c.Add(new SqlBulkCopyColumnMapping(dc.Ordinal, dc.Ordinal));
                    }
                    break;
                case ColumnMatchingMode.CaseSensitiveName:
                    foreach (DataColumn dc in dt.Columns)
                    {
                        c.Add(new SqlBulkCopyColumnMapping(dc.ColumnName, dc.ColumnName));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            BulkCopy(dt, destinationTable, c.ToArray());
        }
        public void BulkCopy(DataTable dt, string destinationTable, params string[] destinationColumnNames)
        {
            var columnMaps = destinationColumnNames.Select((s, i) => new SqlBulkCopyColumnMapping(i, s));
            BulkCopy(dt, destinationTable, columnMaps.ToArray());
        }
        public void BulkCopy(DataTable dt, string destinationTable, params SqlBulkCopyColumnMapping[] columnMappings)
        {
            var conn = this.SharedConnection ?? CreateConnection(this.ConnectionString);
            try
            {
                using (var bc = new SqlBulkCopy(conn))
                {
                    bc.DestinationTableName = destinationTable;
                    foreach (var columnMapping in columnMappings)
                    {
                        if (columnMapping != null)
                        {
                            bc.ColumnMappings.Add(columnMapping);
                        }
                    }

                    int totalRows = dt.Rows.Count;
                    bc.NotifyAfter = totalRows / 10;
                    bc.SqlRowsCopied += (s, e) =>
                    {
                        Trace.TraceInformation("{0}/{1} rows copied...", e.RowsCopied, totalRows);
                    };

                    if (this.SharedConnection != conn) conn.Open();

                    try
                    {
                        bc.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {
                        var e = new SqlBulkCopyExceptionHelper(this.ConnectionString, bc, ex, dt);
                        Exception newEx;
                        if (e.TryHandle(out newEx))
                        {
                            throw newEx;
                        }
                        else
                        {
                            throw;
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
    }
}
