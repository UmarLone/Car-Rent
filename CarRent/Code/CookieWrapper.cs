/****************************** Module Header ******************************\
Module Name:  <CookieWrapper>
Project:      CarRent
Author :      Umar Farooq
\***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Codes
{
	public class CookieWrapper
	{
		private const string ApplicationName = "CarRent";

		private enum CookieItem
		{
			RoleIds,
			PageSize,
			AdminId,
			DateTime,
			TestLinkId,
			FrontUserId,
		}

		/**************
        All cookie values are accessible by public static methods.
        No typos/duplicates are possible from calling code!
        **************/
		/// <summary>
		/// user Cookies as create by user
		/// </summary>
		#region 
		public static DateTime? LoginTime
		{
			get { return Convert.ToDateTime(string.IsNullOrWhiteSpace(GetCookieVal(CookieItem.DateTime)) ? null : GetCookieVal(CookieItem.DateTime)); }
			set { UpdateCookieVal(CookieItem.DateTime, value.ToString(), 10); }
		}

		public static int PageSize
		{
			get { return Convert.ToInt32(string.IsNullOrWhiteSpace(GetCookieVal(CookieItem.PageSize)) ? "10" : GetCookieVal(CookieItem.PageSize)); }
			set { UpdateCookieVal(CookieItem.PageSize, value.ToString(), 10); }
		}

		public static string RoleIds
		{
			get { return GetCookieVal(CookieItem.RoleIds); }
			set { UpdateCookieVal(CookieItem.RoleIds, value.ToString(), 10); }

		}

		public static long AdminId
		{
			get { return Convert.ToInt64(string.IsNullOrWhiteSpace(GetCookieVal(CookieItem.AdminId)) ? "0" : GetCookieVal(CookieItem.AdminId)); }
			set { UpdateCookieVal(CookieItem.AdminId, value.ToString(), 10); }

		}

		public static long FrontUserId
		{
			get { return Convert.ToInt64(string.IsNullOrWhiteSpace(GetCookieVal(CookieItem.FrontUserId)) ? "0" : GetCookieVal(CookieItem.FrontUserId)); }
			set { UpdateCookieVal(CookieItem.FrontUserId, value.ToString(), 10); }

		}

		public static long TestLinkId
		{
			get { return Convert.ToInt64(string.IsNullOrWhiteSpace(GetCookieVal(CookieItem.TestLinkId)) ? "0" : GetCookieVal(CookieItem.TestLinkId)); }
			set { UpdateCookieVal(CookieItem.TestLinkId, value.ToString(), 10); }

		}

		#endregion

		/// <summary>
		/// Get cookies from Client Machine and create cookies is not exist
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private static string GetCookieVal(CookieItem item)
		{
			HttpCookie cookie = GetAppCookie(false); //get the existing cookie
			return (cookie != null && (cookie.Values[item.ToString()] != null))//value or empty if doesn't exist
				? cookie.Values[item.ToString()]
				: string.Empty;
		}

		/// <summary>
		/// Update cookies values
		/// </summary>        
		private static void UpdateCookieVal(CookieItem item, string val, int expireDays)
		{
			//get the existing cookie (or new if not exists)
			HttpCookie cookie = GetAppCookie(true);

			//modify its contents & meta.
			cookie.Expires = DateTime.Now.AddDays(expireDays);
			cookie.Values[item.ToString()] = val;

			//add back to the http response to send back to the browser
			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// Get cookie value
		/// </summary>
		/// <param name="createIfDoesntExist"></param>
		/// <returns></returns>
		private static HttpCookie GetAppCookie(bool createIfDoesntExist)
		{
			if (HttpContext.Current == null)
				return null;
			//get the cookie or a new one if indicated
			return HttpContext.Current.Request.Cookies[ApplicationName] ?? ((createIfDoesntExist) ? new HttpCookie(ApplicationName) : null);
		}
	}
}