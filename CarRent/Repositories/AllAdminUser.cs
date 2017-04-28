/*********************************************
Author Name:   Umar Farooq
Date:           03 Feb 2014, 10:40 PM
/********************************************/

using Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
	public class AllAdminUser
	{
		protected readonly int pageSize = ConfigurationWrapper.PageSize;
		protected readonly int page;
		public AllAdminUser()
		{
			page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
		}
		Repository db = new Repository();
		public AdminUser FinByUser(string UserName, string Password)
		{
			return db.AdminUsers.Where(d => d.UserName == UserName && d.Password == Password && d.IsActive == true && d.IsDelete == false).FirstOrDefault();
		}
		public AdminUser FinById(long AdminId)
		{
			var data = db.AdminUsers.Where(d => d.AdminID == AdminId).FirstOrDefault();
			if (data == null)
				data = new AdminUser();
			return data;
		}
		public AdminUser Find()
		{
			var data = db.AdminUsers.FirstOrDefault();
			if (data == null)
				data = new AdminUser();
			return data;
		}
		public AdminUser FinByEmailId(string EmailId)
		{
			var data = db.AdminUsers.Where(d => d.EmailId.Trim().ToLower() == EmailId).FirstOrDefault();
			if (data == null)
				data = new AdminUser();
			return data;
		}
		public void Delete(long? ID)
		{
			var Admin = db.AdminUsers.Where(d => d.AdminID == ID).FirstOrDefault();
			Admin.UpdateDate = DateTime.Now;
			Admin.IsDelete = true;
			Admin.IsActive = false;
			db.AdminUsers.Update(c => c = Admin);
			db.SaveChanges();
		}
		public long Update(AdminUser m)
		{
			if (m.AdminID > 0)
			{
				m.UpdateBy = m.AdminID;
				m.UpdateDate = DateTime.Now;
				db.AdminUsers.Update(c => c = m);
			}
			db.SaveChanges();
			return m.AdminID;
		}

		public bool CreateAdmin()
		{
			bool chk = false;
			if (!db.AdminUsers.Any())
			{
				var m = db.AdminUsers.Add(new AdminUser(
										0,
										"admin",
										"admin",
									   null  ,
										DateTime.Now,
										0,
										null, 0, true, false, "admin@admin.com"
									   )
						   );

				db.SaveChanges();
				if (m.AdminID > 0)
					chk = true;
			}
			return chk;
		}
	}
}