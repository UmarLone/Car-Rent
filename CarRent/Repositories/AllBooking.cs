/*********************************************
Author Name:      Umar Farooq
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
	public class AllBooking
	{
		protected readonly int pageSize = ConfigurationWrapper.PageSize;
		protected readonly int page;
		public AllBooking()
		{
			page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
		}
		Repository db = new Repository();

		public long Save(Booking m)
		{
			long chk = 0;

			if (m.BookingId > 0)
			{
				Update(m);
			}
			else
			{
				m = db.Booking.Add(new Booking(
										m.BookingId,
										m.CarId,
										m.RentalPlanId,
										m.Name,
										m.SurName,
										m.Email,
										m.HomePhone,

										m.HouseNumber,
										m.Street,
										m.Town,
										m.PostCode,
										m.StartDate,
										m.StartTime,
										m.StartDeopt,
										m.ReturnDate,
										m.ReturnTime,
										m.ReturnDeopt,
										m.Status,
										m.IsActive,
										m.IsDelete,
										m.EntryBy,
										m.UpdateBy,
										m.EntryDate,
										m.UpdateDate,
										m.Type
									   )
						   );
			}
			db.SaveChanges();
			chk = m.BookingId;
			return chk;
		}

		public IEnumerable<Booking> FindByAll(out long TotalPages, string searchString)
		{
			var d = db.Booking.Include("Car").Include("RentalPlan").Where(c => c.IsDelete == false);
			if (!string.IsNullOrEmpty(searchString))
			{
				d = d.Where(c => c.Name.ToLower().Contains(searchString.ToLower()) || c.Email.ToLower().Contains(searchString.ToLower()) || c.SurName.ToLower().Contains(searchString.ToLower()));
			}
			TotalPages = d.Count();
			d = d.OrderByDescending(x => x.BookingId);
			return d.Page(pageSize, page);

		}
		public Booking FinById(long Id)
		{
			var dd = db.Booking.Include("Car").Include("RentalPlan").Where(d => d.BookingId == Id).FirstOrDefault();
			if (dd == null)
				dd = new Booking();
			return dd;
		}

		public IEnumerable<Booking> FinByCarId(long CarId)
		{
			var d = db.Booking.Include("Car").Include("RentalPlan").Where(c => c.IsDelete == false && c.CarId == CarId);
			d = d.OrderByDescending(x => x.BookingId);
			return d;
		}

		public void Delete(long? ID)
		{
			var Admin = db.Booking.Where(d => d.BookingId == ID).FirstOrDefault();
			Admin.UpdateDate = DateTime.Now;
			Admin.IsDelete = true;
			Admin.IsActive = false;
			db.Booking.Update(c => c = Admin);
			db.SaveChanges();
		}
		public long Update(Booking m)
		{
			if (m.BookingId > 0)
			{
				m.UpdateBy = m.BookingId;
				m.UpdateDate = DateTime.Now;
				db.Booking.Update(c => c = m);
			}
			db.SaveChanges();
			return m.BookingId;
		}

		public IEnumerable<Booking> GetAll()
		{
			var d = db.Booking.Include("Car").Include("RentalPlan").Where(c => c.IsDelete == false);
			return d;
		}
	}
}