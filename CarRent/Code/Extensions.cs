/****************************** Module Header ******************************\
Module Name:  <Extension Files>
Project:      CarRent
Author :     Umar Farooq
\***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Data;
using System.Web.Routing;
using System.Linq.Expressions;
using System.Web.Mvc.Html;

namespace Codes
{
    /// <summary>
    /// These are some common use functions
    /// </summary>
    #region Common Use Functions   
    
    public static class SystemTime
    {
        public static DateTime Now
        {
            get
            {
                //DateTime timeUtc = DateTime.UtcNow;
                //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
                //return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                return DateTime.Now;
            }
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Auto Generate Code
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            Random _random = new Random(Environment.TickCount);

            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder builder = new StringBuilder(length);

            for (int i = 0; i < length; ++i)
                builder.Append(chars[_random.Next(chars.Length)]);

            return builder.ToString();
        }

        /// <summary>
        /// Create a MVC string
        /// </summary>        
        public static MvcHtmlString OrderBy(this  System.Web.Mvc.HtmlHelper helper, string columnName, string action, string sortColumn, NameValueCollection queryStrings)
        {
            RouteValueDictionary routeValues = queryStrings.ToRouteValueDictionary();

            IDictionary<string, object> htmlAttributes = new Dictionary<string, object>();

            var direction = Sort.Descending;

            Enum.TryParse<Sort>(queryStrings["direction"], out direction);

            if (queryStrings.AllKeys.Any(c => c == "direction"))
                routeValues["direction"] = Sort.Ascending != direction ? Sort.Ascending : Sort.Descending;
            else
                routeValues.Add("direction", Sort.Ascending != direction ? Sort.Ascending : Sort.Descending);

            if (queryStrings.AllKeys.Any(c => c == "sortColumn"))
                routeValues["sortColumn"] = sortColumn;
            else
                routeValues.Add("sortColumn", sortColumn);


            htmlAttributes.Add("class", Sort.Ascending != direction ? "sort_asc" : "sort_desc");

            return LinkExtensions.ActionLink(helper, columnName, action, routeValues, htmlAttributes);
        }

        public static RouteValueDictionary ToRouteValueDictionary(this NameValueCollection c)
        {
            RouteValueDictionary r = new RouteValueDictionary();
            foreach (string s in c.AllKeys)
            {
                r.Add(s, c[s]);
            }
            return r;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> data, string fieldName, Sort sortOrder = Sort.Descending)
        {

            return (sortOrder == Sort.Descending) ? ApplyOrder<T>(data, fieldName, "OrderByDescending")
                : ApplyOrder<T>(data, fieldName, "OrderBy");
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);

                if (pi == null)
                    pi = type.GetProperties(BindingFlags.Public |
                                    BindingFlags.Instance).First();

                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        /// <summary>
        /// Replace string value
        /// </summary>        
        public static String PregReplace(this String input, string[] pattern, string[] replacements)
        {
            if (replacements.Length != pattern.Length)
                throw new ArgumentException("Replacement and Pattern Arrays must be balanced");

            for (var i = 0; i < pattern.Length; i++)
            {
                input = Regex.Replace(input, pattern[i], replacements[i]);
            }

            return input;
        }

        public static string ToJSON(this System.Web.Mvc.FormCollection collection)
        {
            var list = new Dictionary<string, string>();
            foreach (string key in collection.Keys)
            {
                list.Add(key, collection[key]);
            }
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);
        }

        public static void ToLowerCase(this RouteCollection routes)
        {
            for (int i = 0; i < RouteTable.Routes.Count; i++)
            {
                RouteTable.Routes[i] = new LowerCaseRouteDecorator(RouteTable.Routes[i]);
            }
        }

        public static string Between(this string src, string findFrom, string findTo)
        {
            int start = src.IndexOf(findFrom);
            int to = src.IndexOf(findTo, start + findFrom.Length + 1);
            if (start < 0 || to < 0) return "";
            string s = src.Substring(
                           start + findFrom.Length,
                           to - start - findFrom.Length);
            return s;
        }

        /// <summary>
        /// 
        /// </summary>       
        public static byte[] FileToBytes(this string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> source, params T[] items)
        {
            return source.Concat(items);
        }

        public static string HtmlStrip(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input.IfNullOrEmpty("");
            input = Regex.Replace(input, "<style>(.|\n)*?</style>", string.Empty);
            input = Regex.Replace(input, @"<xml>(.|\n)*?</xml>", string.Empty); // remove all <xml></xml> tags and anything inbetween.  
            return Regex.Replace(input, @"<(.|\n)*?>", string.Empty); // remove any tags but not there content "<p>bob<span> johnson</span></p>" becomes "bob johnson"

        }

        public static string RemoveSpecial(this string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                Regex re = new Regex("[;\\/:*,-?\"<>|&']");
                return re.Replace(s, " "); ;
            }
            else
                return s;

        }

        [Description("Retrieves an IEnumerable<SelectListItem> from type of enum.")]
        public static IEnumerable<SelectListItem> SelectList<T>(this Type o, bool isDesc = false)
        {
            if (o.IsEnum)
            {
                return Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem()
                {
                    Text = isDesc ? (v.GetType()
                                    .GetField(v.ToString())
                                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                    .SingleOrDefault() as DescriptionAttribute).Description
                                 : v.ToString(),
                    Value = Convert.ToInt32(v).ToString()
                });
            }
            return null;

        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }
             
        public static IEnumerable<SelectListItem> Intgers(this int from, int to)
        {
            var i = from;
            while (i <= to)
            {
                var s = new SelectListItem();
                s.Text = i.ToString();
                s.Value = i.ToString();
                i++;
                yield return s;
            }
        }

        public static string Desciption(this Enum o)
        {
            DescriptionAttribute attribute = o.GetType()
                .GetField(o.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? o.ToString() : attribute.Description;
        }

        public static Uri AddQueryString(this Uri uri, string key, dynamic value)
        {
            value = Convert.ToString(value);
            var urlB = new UriBuilder(uri);
            if (string.IsNullOrEmpty(urlB.Query))
            {
                if (!string.IsNullOrEmpty(value)) urlB.Query = string.Format("{0}={1}", key, value);
            }
            else
            {
                var q = urlB.Query.Substring(1); // Remove leading ?
                var nvc = HttpUtility.ParseQueryString(q);
                if (string.IsNullOrEmpty(value))
                {
                    if (nvc.AllKeys.Contains(key)) nvc.Remove(key);
                }
                else
                {
                    nvc[key] = value.ToString();
                }
                urlB.Query = nvc.ToString(); // Overridden HttpValueCollection
            }
            return urlB.Uri;
        }

        public static T GetSafe<T>(this System.Data.IDataReader dr, int column)
        {
            var o = dr[column];
            if (o == null || o == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)o;
            }
        }

        public static T GetSafe<T>(this System.Data.IDataReader dr, string column)
        {
            var o = dr[column];
            if (o == null || o == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)o;
            }
        }

        public static T Get<T>(this System.Data.IDataReader dr, int column)
        {
            var o = dr[column];
            return (T)o;
        }

        public static T Get<T>(this System.Data.IDataReader dr, string column)
        {
            var o = dr[column];
            return (T)o;
        }

        public static bool IsDBNull(this IDataReader dr, string column)
        {
            return dr.IsDBNull(dr.GetOrdinal(column));
        }

        public static bool HasColumn(this IDataReader dr, string column)
        {
            var dv = dr.GetSchemaTable().DefaultView;
            dv.RowFilter = string.Format("ColumnName= '{0}'", column);
            return dv.Count > 0;
        }

        public static HttpResponse Write(this HttpResponse response, params string[] values)
        {
            foreach (var s in values)
            {
                response.Write(s);
            }
            return response;
        }

        public static object DBNullIfNullOrEmpty(this string s)
        {
            if (string.IsNullOrEmpty(s)) return DBNull.Value;
            return s;
        }

        public static object DBNullIfDefault<T>(this T o)
        {
            if (object.Equals(o, default(T))) return DBNull.Value;
            return o;
        }

        public static string NullIfEmpty(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            return s;
        }

        public static T TryParse<T>(this string value, TryParseHandler<T> handler) where T : struct
        {
            if (String.IsNullOrEmpty(value))
                return default(T);
            T result;
            if (handler(value, out result))
                return result;

            return default(T);
        }

        public delegate bool TryParseHandler<T>(string value, out T result);

        public static string SplitWordsToSentence(this string source)
        {
            return string.Join(" ", SplitWords(source));
        }

        private static string[] SplitWords(this string source)
        {
            if (source == null) return new string[] { }; //Return empty array.
            if (source.Length == 0) return new string[] { "" };

            StringCollection words = new StringCollection();
            int wordStartIndex = 0;
            char[] letters = source.ToCharArray();
            char previousChar = char.MinValue;

            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar))
                {
                    //Grab everything before the current character.
                    words.Add(new String(letters, wordStartIndex, i - wordStartIndex));
                    wordStartIndex = i;
                }
                previousChar = letters[i];
            }

            //We need to have the last word.
            words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex));

            string[] wordArray = new string[words.Count];
            words.CopyTo(wordArray, 0);
            return wordArray;
        }

        public static double InWeeks(this TimeSpan t)
        {
            return t.TotalDays / 7;
        }

        public static double InYears(this TimeSpan t)
        {
            return t.TotalDays / 365;
        }

        public static int MonthsBetween(this DateTime startDate, DateTime endDate)
        {
            DateTime cur = startDate;
            var d = 0;
            for (int i = 0; cur <= endDate; cur = startDate.AddMonths(++i))
            {
                d++;
            }
            return d + 1;
        }

        public static string TruncateAt(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (s.Length <= maxLength) return s;

            if (s.Length > 3 && maxLength <= 3) return "...";
            return s.Substring(0, maxLength - 3) + "...";
        }

        public static int FirstIndexOf<T>(this IEnumerable<T> e, Func<T, bool> predicate)
        {
            return e.TakeWhile(predicate).Count() - 1;
        }

        public static string TitleCase(this string s)
        {
            var ti = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
            // lowercase to remove any acronyms
            return ti.ToTitleCase(s.ToLower());
        }

        public static string IfNullOrEmpty(this string @this, string s)
        {
            return string.IsNullOrEmpty(@this) ? s : @this;
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, int currentPage, int currentPageSize, int totalRecords)
        {
            if (totalRecords < currentPageSize)
                return null;

            StringBuilder sb = new StringBuilder();
            var uri = HttpContext.Current.Request.Url;

            int seed = currentPage % currentPageSize == 0 ? currentPage : currentPage - (currentPage % currentPageSize);

            if (currentPage > 1)
                sb.AppendLine(String.Format("<a href=\"{0}\">Previous</a>", uri.AddQueryString("page", currentPage - 1)));

            if (currentPage - currentPageSize >= 0)
                sb.AppendLine(String.Format("<a href=\"{0}\">...</a>", uri.AddQueryString("page", (currentPage - currentPageSize) + 1)));

            for (int i = seed; i < Math.Ceiling(double.Parse(totalRecords.ToString()) / double.Parse(currentPageSize.ToString())) && i < seed + currentPageSize; i++)
                sb.AppendLine(String.Format("<a href=\"{0}\" class=\"{2}\">{1}</a>", uri.AddQueryString("page", i + 1), i + 1, currentPage == i + 1 ? "selected" : ""));

            if (currentPage + currentPageSize <= (Math.Ceiling(double.Parse(totalRecords.ToString()) / double.Parse(currentPageSize.ToString()))))
                sb.AppendLine(String.Format("<a href=\"{0}\" >...</a>", uri.AddQueryString("page", (currentPage + currentPageSize) + 1)));

            if (currentPage < (Math.Ceiling(double.Parse(totalRecords.ToString()) / double.Parse(currentPageSize.ToString()))))
                sb.AppendLine(String.Format("<a href=\"{0}\">Next</a>", uri.AddQueryString("page", currentPage + 1)));

            return new MvcHtmlString(sb.ToString());
        }


        public static MvcHtmlString HomePager(this HtmlHelper helper, int currentPage, int currentPageSize, int totalRecords)
        {
            if (totalRecords < currentPageSize)
                return null;

            StringBuilder sb = new StringBuilder();
            var uri = HttpContext.Current.Request.Url;

            int seed = currentPage % currentPageSize == 0 ? currentPage : currentPage - (currentPage % currentPageSize);

            if (currentPage > 1)
                sb.AppendLine(String.Format(" <li><a href=\"{0}\">Previous</a> </li>", uri.AddQueryString("page", currentPage - 1)));

            if (currentPage - currentPageSize >= 0)
                sb.AppendLine(String.Format("<li><a  href=\"{0}\">...</a></li>", uri.AddQueryString("page", (currentPage - currentPageSize) + 1)));

            for (int i = seed; i < Math.Ceiling(double.Parse(totalRecords.ToString()) / double.Parse(currentPageSize.ToString())) && i < seed + currentPageSize; i++)
                sb.AppendLine(String.Format("<li><a href=\"{0}\" class=\"{2}\" >{1}</a></li>", uri.AddQueryString("page", i + 1), i + 1, currentPage == i + 1 ? "active" : ""));

            if (currentPage + currentPageSize <= (Math.Ceiling(double.Parse(totalRecords.ToString()) / double.Parse(currentPageSize.ToString()))))
                sb.AppendLine(String.Format("<li><a href=\"{0}\" >...</a></li>", uri.AddQueryString("page", (currentPage + currentPageSize) + 1)));

            if (currentPage < (Math.Ceiling(double.Parse(totalRecords.ToString()) / double.Parse(currentPageSize.ToString()))))
                sb.AppendLine(String.Format("<li><a  href=\"{0}\">Next</a></li>", uri.AddQueryString("page", currentPage + 1)));

            return new MvcHtmlString(sb.ToString());
        }

        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int page)
        {
            return en.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int page)
        {
            return en.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static void Update<TSource>(this IEnumerable<TSource> outer, Action<TSource> updator)
        {
            foreach (TSource item in outer)
            {
                updator(item);
            }
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            // Create the result table, and gather all properties of a T        
            DataTable table = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add the properties as columns to the datatable
            foreach (var prop in props)
            {
                Type propType = prop.PropertyType;

                // Is it a nullable type? Get the underlying type 
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    propType = new NullableConverter(propType).UnderlyingType;

                table.Columns.Add(prop.Name, propType);
            }

            // Add the property values per T as rows to the datatable
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                table.Rows.Add(values);
            }

            return table;
        }

        public static IEnumerable<string> ToCsv<T>(this IEnumerable<T> objectlist, string separator)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            PropertyInfo[] properties = typeof(T).GetProperties();
            yield return String.Join(separator, fields.Select(f => f.Name).Union(properties.Select(p => p.Name)).ToArray());
            foreach (var o in objectlist)
            {
                yield return string.Join(separator, fields.Select(f => (f.GetValue(o) ?? "").ToString())
                    .Union(properties.Select(p => (p.GetValue(o, null) ?? "").ToString())).ToArray());
            }
        }

        public static string GeneratePolicyID(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

    }

    internal class LowerCaseRouteDecorator : RouteBase, IRouteWithArea
    {
        private readonly RouteBase _innerRoute;

        public LowerCaseRouteDecorator(RouteBase innerRoute)
        {
            _innerRoute = innerRoute;
        }

        public RouteBase InnerRoute
        {
            get { return _innerRoute; }
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            return _innerRoute.GetRouteData(httpContext);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData path = _innerRoute.GetVirtualPath(requestContext, values);

            if (path != null && path.VirtualPath != null)
            {
                string virtualPath = path.VirtualPath;
                path.VirtualPath = ToLowerVirtualPath(virtualPath);
            }

            return path;
        }

        private static string ToLowerVirtualPath(string virtualPath)
        {
            var lastIndexOf = virtualPath.LastIndexOf("?", StringComparison.OrdinalIgnoreCase);

            if (lastIndexOf < 0)
            {
                return virtualPath.ToLowerInvariant();
            }

            string path = virtualPath.Substring(0, lastIndexOf).ToLowerInvariant();
            string query = virtualPath.Substring(lastIndexOf);
            return path + query;
        }

        public string Area
        {
            get
            {
                string area = GetAreaToken(_innerRoute);
                return area;
            }
        }

        private string GetAreaToken(RouteBase routeBase)
        {
            var route = routeBase as Route;
            if (route == null || route.DataTokens == null)
            {
                return null;
            }
            return route.DataTokens["area"] as string;
        }
    }
    #endregion
}
