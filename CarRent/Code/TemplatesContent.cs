
/****************************** Module Header ******************************\
Module Name:  Creating Template for emails to user and jollyGood 
Project:      CarRent
Author :     Umar Farooq
\***************************************************************************/



using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Web;

namespace Codes
{
	public class TemplatesContent
	{
		public static string BookingTemplate(string Header = "", string UserName = "", string DynamicText = "", string Link = "", string Footer = "", string VehicleType = "", string VehicleName = "", string Plan = "", string StartDate = "", string StartTime = "", string StartDepot = "", string ReturnDate = "", string ReturnTime = "", string ReturnDepot = "", string Name = "", string Email = "", string Address = "", string Mobile = "")
		{
			StringBuilder strBuilder = new StringBuilder();
			string content = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\Content\themenew\EmailTempletes\Booking.html"));

			StringBuilder _tempate = new StringBuilder();
			_tempate.Append(content);
			_tempate = _tempate
							   .Replace("{UserName}", UserName)
							   .Replace("{Name}", Name)
							   .Replace("{Email}", Email)
							   .Replace("{Mobile}", Mobile)
							   .Replace("{Address}", Address)
							   .Replace("{VehicleType}", VehicleType)
							   .Replace("{VehicleName}", VehicleName)
							   .Replace("{Plan}", Plan)
							   .Replace("{StartDate}", StartDate)
							   .Replace("{StartTime}", StartTime)
							   .Replace("{StartDepot}", StartDepot)
							   .Replace("{ReturnDate}", ReturnDate)
							   .Replace("{ReturnTime}", ReturnTime)
							   .Replace("{ReturnDepot}", ReturnDepot)
							   .Replace("{Header}", Header)
							   .Replace("{DynamicText}", DynamicText)
							   .Replace("{Link}", Link)
							   .Replace("{Footer}", Footer);
			return _tempate.ToString();
		}

		public static string CommonTemplate(string Header = "", string UserName = "", string DynamicText = "", string Link = "", string Footer = "", string VehicleType = "", string VehicleName = "", string Plan = "", string StartDate = "", string StartTime = "", string StartDepot = "", string ReturnDate = "", string ReturnTime = "", string ReturnDepot = "", string Name = "", string Email = "", string Address = "", string Mobile = "")
		{
			StringBuilder strBuilder = new StringBuilder();
			string content = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\Content\themenew\EmailTempletes\CommonTemplate.html"));

			StringBuilder _tempate = new StringBuilder();
			_tempate.Append(content);
			_tempate = _tempate
							   .Replace("{UserName}", UserName)
							   .Replace("{Name}", Name)
							   .Replace("{Email}", Email)
							   .Replace("{Mobile}", Mobile)
							   .Replace("{Address}", Address)
							   .Replace("{VehicleType}", VehicleType)
							   .Replace("{VehicleName}", VehicleName)
							   .Replace("{Plan}", Plan)
							   .Replace("{StartDate}", StartDate)
							   .Replace("{StartTime}", StartTime)
							   .Replace("{StartDepot}", StartDepot)
							   .Replace("{ReturnDate}", ReturnDate)
							   .Replace("{ReturnTime}", ReturnTime)
							   .Replace("{ReturnDepot}", ReturnDepot)
							   .Replace("{Header}", Header)
							   .Replace("{DynamicText}", DynamicText)
							   .Replace("{Link}", Link)
							   .Replace("{Footer}", Footer);
			return _tempate.ToString();
		}

	}
}