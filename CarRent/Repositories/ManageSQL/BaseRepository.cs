using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Repositories.ManageSQL;

namespace Repositories
{
    public abstract class BaseRepository 
    {
        protected SqlHelper sqlH { get; set; }

        protected BaseRepository()
            : this(SqlHelper.GetConnectionString("Repository"))
        {
        }

        protected BaseRepository(string connectionString)
            : this(new SqlHelper(connectionString))
        {
        }

        protected BaseRepository(SqlHelper sqlHelper)
        {
            this.sqlH = sqlHelper;
        }

    }
}
