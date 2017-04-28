/****************************** Module Header ******************************\
Module Name:  <TimeZoneConverter>
Project:      CarRent
Author :     Umar Farooq
\***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Codes
{
    public class TimeZoneConverter
    {
        /// <summary>
        /// function that show user time zone according to time zone
        /// </summary>      
        public static DateTime GetTime(string TimeZone)
        {
            DateTime targetTime = DateTime.Now;
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                if (TimeZone.ToLower() == (z.DisplayName.Contains("UTC") ? z.DisplayName.Replace("UTC", "GMT").ToLower() : z.DisplayName.ToLower()))
                {
                    TimeZoneInfo timeZone = z;
                    targetTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
                }
            }
            return targetTime;
        }
    }
}