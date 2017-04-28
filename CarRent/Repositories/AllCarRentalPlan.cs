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
	public class AllCarRentalPlan
	{
		protected readonly int pageSize = ConfigurationWrapper.PageSize;
		protected readonly int page;
		public AllCarRentalPlan()
		{
			page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
		}
		Repository db = new Repository();

		public long Save(CarRentalPlan m)
		{
			long chk = 0;

			if (m.CarRentalPlanId > 0)
			{
				Update(m);
			}
			else
			{
				m = db.CarRentalPlan.Add(new CarRentalPlan(
										m.CarRentalPlanId,
										m.CarId,
										m.RentalPlanId,
										m.IsActive,
										m.IsDelete,
										m.EntryBy,
										m.UpdateBy,
										m.EntryDate,
										m.UpdateDate,
										m.Price
									   )
						   );
			}
			db.SaveChanges();
			chk = m.CarRentalPlanId;
			return chk;
		}

		public IEnumerable<CarRentalPlan> FindByAll(out long TotalPages)
		{
			var d = db.CarRentalPlan.Include("Car").Include("RentalPlan").Where(c => c.IsDelete == false);
			TotalPages = d.Count();
			d = d.OrderByDescending(x => x.CarRentalPlanId);
			return d.Page(pageSize, page);

		}
		public CarRentalPlan FinById(long Id)
		{
			return db.CarRentalPlan.Include("Car").Include("RentalPlan").Where(d => d.CarRentalPlanId == Id).FirstOrDefault();
		}

		public IEnumerable<CarRentalPlan> FinByCarId(long CarId)
		{
			var d = db.CarRentalPlan.Include("Car").Include("RentalPlan").Where(c => c.IsDelete == false && c.CarId == CarId);
			d = d.OrderByDescending(x => x.CarRentalPlanId);
			return d;
		}

		public void Delete(long? ID)
		{
			var Admin = db.CarRentalPlan.Where(d => d.CarRentalPlanId == ID).FirstOrDefault();
			Admin.UpdateDate = DateTime.Now;
			Admin.IsDelete = true;
			Admin.IsActive = false;
			db.CarRentalPlan.Update(c => c = Admin);
			db.SaveChanges();
		}
		public long Update(CarRentalPlan m)
		{
			if (m.CarRentalPlanId > 0)
			{
				m.UpdateBy = m.CarRentalPlanId;
				m.UpdateDate = DateTime.Now;
				db.CarRentalPlan.Update(c => c = m);
			}
			db.SaveChanges();
			return m.CarRentalPlanId;
		}

		public IEnumerable<CarRentalPlan> GetAll()
		{
			var d = db.CarRentalPlan.Include("Car").Include("RentalPlan").Where(c => c.IsDelete == false);
			return d;
		}
	}
}