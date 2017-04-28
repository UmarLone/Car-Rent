/****************************** Module Header ******************************\
Module Name:  <SessionWrapper>
Project:      CarRent
Author :      Umar Farooq
\***************************************************************************/
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Codes
{
    public static class SessionWrapper
    {
        //------ Session created by users ---------------------------------
        #region User Sessions

        public static long UserId
        {
            get { return GetFromSession<long>("UserId"); }
            set { SetInSession<long>("UserId", value); }
        }

       

        public static DateTime? LoginTime
        {
            get { return GetFromSession<DateTime?>("LoginTime"); }
            set { SetInSession<DateTime?>("LoginTime", value); }
        }

        public static string CurrencySymbol
        {
            get { return GetFromSession<string>("CurrencySymbol"); }
            set { SetInSession<string>("CurrencySymbol", value); }
        }

        public static IEnumerable<int> RoleIds
        {
            get { return GetFromSession<IEnumerable<int>>("RoleIds"); }
            set { SetInSession<IEnumerable<int>>("RoleIds", value); }
        }

        public static AdminUser Admin
        {
            get { return GetFromSession<AdminUser>("User"); }
            set { SetInSession<AdminUser>("User", value); }
        }

        #endregion

        //------------------ Session Values functions ---------------------
        #region these function handle session on server

        private static T GetFromSession<T>(string key)
        {
            object obj = HttpContext.Current.Session[key];
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }

        private static void SetInSession<T>(string key, T value)
        {
            if (value == null)
            {
                HttpContext.Current.Session.Remove(key);
            }
            else
            {
                HttpContext.Current.Session[key] = value;
            }
        }

        private static T GetFromApplication<T>(string key)
        {
            return (T)HttpContext.Current.Application[key];
        }

        private static void SetInApplication<T>(string key, T value)
        {
            if (value == null)
            {
                HttpContext.Current.Application.Remove(key);
            }
            else
            {
                HttpContext.Current.Application[key] = value;
            }
        }

        #endregion
    }
    
}