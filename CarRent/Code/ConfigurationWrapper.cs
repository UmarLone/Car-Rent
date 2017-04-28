/****************************** Module Header ******************************\
Module Name:  <Configuration Manager>
Project:      CarRent
Author :      Umar Farooq
\***************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Configuration;
using Codes;
namespace Codes
{
	public class ConfigurationWrapper
	{
		public static int PageSize
		{
			get { return GetValue<int>("PageSize"); }
		}

		/// <summary>
		/// Key Values in respective of Webconfig Keys
		/// </summary>
		#region
		public static string WebUrl
		{
			get { return GetValue<string>("WebUrl"); }
		}

		public static string ChatXMLPath
		{
			get { return GetValue<string>("ChatXML"); }
		}

		public static string UploadedLogo
		{
			get { return GetValue<string>("UploadedLogo"); }
		}

		public static string GalleryPhoto
		{
			get { return GetValue<string>("GalleryPhoto"); }
		}

		public static string UserPics
		{
			get { return GetValue<string>("UserPics"); }
		}

		public static string SampleTheme
		{
			get { return GetValue<string>("SampleTheme"); }
		}
		public static string SMTP_SERVER
		{
			get { return GetValue<string>("SMTP_SERVER"); }
		}

		public static string SMTP_USER
		{
			get { return GetValue<string>("SMTP_USER"); }
		}
		public static string SMTP_PASSWORD
		{
			get { return GetValue<string>("SMTP_PASSWORD"); }
		}
		public static string SMTP_FROM
		{
			get { return GetValue<string>("SMTP_FROM"); }
		}

		public static int SMTP_PORT
		{
			get { return GetValue<int>("SMTP_PORT"); }
		}
		#endregion

		/// <summary>
		/// Function to get value from Webconfig
		/// </summary>        
		public static T GetValue<T>(string key)
		{
			var o = TypeDescriptor.GetConverter(typeof(T));
			if (o != null)
			{
				return (T)o.ConvertFromString(ConfigurationManager.AppSettings[key]);
			}
			return default(T);
		}
	}
}