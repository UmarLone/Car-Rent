using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics;

namespace Repositories.ManageSQL
{
    public enum ColumnMatchingMode
    {
        Ordinal = 0,
        CaseSensitiveName = 1
    }
}