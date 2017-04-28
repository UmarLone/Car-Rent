/*********************************************
Author Name:    Umar Farooq
Date:           03 Feb 2014, 10:40 PM
/********************************************/

using Codes;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViewModel;
using Models;
using System.IO;

namespace CarRent.Areas.Admin.Controllers
{
	public class BookingController : Controller
	{
		AllBooking _objAll = new AllBooking();
		AllRentalPlan _objRP = new AllRentalPlan();
		AllCar _allCar = new AllCar();
		AllCarRentalPlan _allRental = new AllCarRentalPlan();

		[Authenticate]
		public ActionResult Index(string Search = "")
		{
			if (!string.IsNullOrEmpty(Search))
				ViewBag.Search = Search;
			long total = 0;
			var datalist = _objAll.FindByAll(out total, Search.Trim().ToLower());
			ViewBag.TotalPages = total;
			return View(datalist);
		}

		/// <summary>
		/// This method use to car list  by type.
		/// <summary>
		[AcceptVerbs(HttpVerbs.Post)]
		[Authenticate]
		[ValidateInput(false)]
		public ActionResult GetVehicles(int Type)
		{
			var d = new SelectList(_allCar.GetAll().ToList().FindAll(c => c.Type == Type).Where(c => c.IsActive == true).Select(c => new
			{
				Value = c.CarId,
				Text = c.CarName
			}), "Value", "Text");
			return Json(d, JsonRequestBehavior.AllowGet);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[Authenticate]
		[ValidateInput(false)]
		[HandleError]
		public ActionResult GetRentalPlan(long CarId)
		{
			var d = new SelectList(_allRental.GetAll().ToList().FindAll(c => c.CarId == CarId).Where(c => c.IsActive == true).Select(c => new
			{
				Value = c.RentalPlanId,
				Text = c.RentalPlan.RentalPlanName + "(£" + c.Price + ")"
			}), "Value", "Text");
			return Json(d, JsonRequestBehavior.AllowGet);
		}
		[Authenticate]
		public ActionResult Detail(long Id = 0)
		{
			var _obj = _objAll.FinById(Id);
			var _objV = new BookingViewModel(_obj);

			ViewBag.Cars = new SelectList(_allCar.GetAll().ToList().FindAll(c => c.Type == (_obj != null ? _obj.Type : 0)).Where(c => c.IsActive == true).Select(c => new
			{
				Value = c.CarId,
				Text = c.CarName
			}), "Value", "Text");

			ViewBag.RentalPlans = new SelectList(_allRental.GetAll().ToList().FindAll(c => c.CarId == (_obj != null ? _obj.CarId : 0)).Where(c => c.IsActive == true).Select(c => new
			{
				Value = c.RentalPlanId,
				Text = c.RentalPlan.RentalPlanName + "(£" + c.Price + ")"
			}), "Value", "Text");
			return View(_objV);
		}

		[Authenticate]
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Detail(BookingViewModel Model)
		{
			try
			{
				ViewBag.Cars = new SelectList(_allCar.GetAll().ToList().FindAll(c => c.Type == (Model != null ? Model.Type : 0)).Where(c => c.IsActive == true).Select(c => new
				{
					Value = c.CarId,
					Text = c.CarName
				}), "Value", "Text");

				ViewBag.RentalPlans = new SelectList(_allRental.GetAll().ToList().FindAll(c => c.CarId == (Model != null ? Model.CarId : 0)).Where(c => c.IsActive == true).Select(c => new
				{
					Value = c.RentalPlanId,
					Text = c.RentalPlan.RentalPlanName + "(£" + c.Price + ")"
				}), "Value", "Text");
				if (ModelState.IsValid)
				{
					Booking obj = _objAll.FinById(Model.BookingId);
					obj.BookingId = Model.BookingId;
					obj.CarId = Model.CarId;
					obj.RentalPlanId = Model.RentalPlanId;
					obj.Email = Model.Email;
					obj.Type = Model.Type;
					obj.IsActive = Model.IsActive;
					obj.EntryBy = SessionWrapper.Admin.AdminID;
					obj.EntryDate = DateTime.Now;
					obj.HomePhone = Model.HomePhone;
					obj.HouseNumber = Model.HouseNumber;

					obj.Name = Model.Name;
					obj.PostCode = Model.PostCode;
					obj.ReturnDate = Model.ReturnDate;
					obj.ReturnDeopt = Model.ReturnDeopt;
					obj.ReturnTime = Model.ReturnTime;
					obj.StartDate = Model.StartDate;
					obj.StartDeopt = Model.StartDeopt;
					obj.StartTime = Model.StartTime;
					obj.Status = Model.Status;
					obj.Street = Model.Street;
					obj.SurName = Model.SurName;
					obj.Town = Model.Town;

					long chk = _objAll.Save(obj);

					if (chk > 0)
					{
						ViewBag.Sucess = "Booking Detail saved successfully.";
					}
					else
					{
						ViewBag.Sucess = "Booking failed.";
					}

				}
				else
				{
					return View(Model);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return View(Model);
			}

			return this.Detail(Model.BookingId);
		}

		[Authenticate]
		public ActionResult Delete(long Id = 0)
		{
			TempData["Message"] = "";
			try
			{
				_objAll.Delete(Id);
				TempData["Message"] = "Vehicle deleted successfully.";
			}
			catch
			{
				TempData["Message"] = "Vehicle deletion failed!";
			}
			return RedirectToAction("Index");
		}
	}
}
