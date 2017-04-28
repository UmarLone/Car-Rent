/*********************************************
Author Name:    Umar Farooq
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
	public class AllRentalPlan
	{
		protected readonly int pageSize = ConfigurationWrapper.PageSize;
		protected readonly int page;
		public AllRentalPlan()
		{
			page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
		}
		Repository db = new Repository();

		public long Save(RentalPlan m)
		{
			long chk = 0;
			if (IsNameExists(m.RentalPlanName, m.RentalPlanId))
			{
				chk = -1;
				return chk;
			}
			if (m.RentalPlanId > 0)
			{
				Update(m);
			}
			else
			{
				m = db.RentalPlan.Add(new RentalPlan(
										m.RentalPlanId,
										m.RentalPlanName,
										m.Summary,
										m.IsActive,
										m.IsDelete,
										m.EntryBy,
										m.UpdateBy,
										m.EntryDate,
										m.UpdateDate,
										m.NoOfDays
									   )
						   );
			}
			db.SaveChanges();
			chk = m.RentalPlanId;
			return chk;
		}

		public bool IsNameExists(string RentalPlanName, long RentalPlanId)
		{
			bool chk = false;
			if (RentalPlanId > 0)
			{
				if (db.RentalPlan.Any(c => c.RentalPlanName == RentalPlanName && c.RentalPlanId != RentalPlanId && c.IsDelete == false))
					chk = true;
				else
					chk = false;
			}
			else
			{
				if (db.RentalPlan.Any(c => c.RentalPlanName == RentalPlanName && c.IsDelete == false))
					chk = true;
				else
					chk = false;
			}
			return chk;
		}
		public IEnumerable<RentalPlan> FindByAll(out long TotalPages, string searchString)
		{
			var d = db.RentalPlan.Where(c => c.IsDelete == false);
			if (!string.IsNullOrEmpty(searchString))
			{
				d = d.Where(c => c.RentalPlanName.ToLower().Contains(searchString.ToLower()));
			}
			TotalPages = d.Count();
			d = d.OrderByDescending(x => x.RentalPlanId);
			return d.Page(pageSize, page);

		}
		public RentalPlan FinById(long RentalPlanId)
		{
			var data = db.RentalPlan.Where(d => d.RentalPlanId == RentalPlanId).FirstOrDefault();
			if (data == null)
				data = new RentalPlan();
			return data;
		}
		public void Delete(long? ID)
		{
			var Admin = db.RentalPlan.Where(d => d.RentalPlanId == ID).FirstOrDefault();
			Admin.UpdateDate = DateTime.Now;
			Admin.IsDelete = true;
			Admin.IsActive = false;
			db.RentalPlan.Update(c => c = Admin);
			db.SaveChanges();
		}
		public long Update(RentalPlan m)
		{
			if (m.RentalPlanId > 0)
			{
				m.UpdateBy = m.RentalPlanId;
				m.UpdateDate = DateTime.Now;
				db.RentalPlan.Update(c => c = m);
			}
			db.SaveChanges();
			return m.RentalPlanId;
		}

		public IEnumerable<RentalPlan> GetAll()
		{
			var d = db.RentalPlan.Where(c => c.IsDelete == false);
			return d;
		}
	}
}