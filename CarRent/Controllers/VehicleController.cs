/*********************************************
Author Name:   Umar Farooq
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
using System.Threading.Tasks;

namespace CarRent.Controllers
{
	public class VehicleController : DerivedController
	{
		AllCar _objAll = new AllCar();
		AllCarRentalPlan _objallCR = new AllCarRentalPlan();
		AllRentalPlan _objRP = new AllRentalPlan();
		CarViewModel _objV = new CarViewModel();
		AllBooking _objAllB = new AllBooking();
		AllAdminUser _allAdmin = new AllAdminUser();

		public ActionResult Index(string Search = "", string Car = "", string Van = "")
		{
			try
			{
				int type = 0;
				if (!string.IsNullOrEmpty(Search))
					ViewBag.Search = Search;

				if (!string.IsNullOrEmpty(Car))
					ViewBag.Type = Car.ToLower().Trim();
				if (!string.IsNullOrEmpty(Van))
					ViewBag.Type = Van.ToLower().Trim();

				if (!string.IsNullOrEmpty(ViewBag.Type))
				{
					if (Convert.ToString(ViewBag.Type) == "car")
					{
						type = 1;
					}
					else if (Convert.ToString(ViewBag.Type) == "van")
					{
						type = 2;
					}

				}
				long total = 0;
				_objV.Cars = _objAll.FindByAllForFront(out total, Search.Trim().ToLower(), type);
				ViewBag.TotalPages = total;
			}
			catch
			{
			}
			return View(_objV);
		}


		public ActionResult Detail(long Id = 0)
		{
			try
			{
				_objV = new CarViewModel(_objAll.FinById(Id));
				_objV.PlanName = _objAll.GetPlanNameByCarId(Id);
			}
			catch
			{
			}
			return View(_objV);
		}






		/// <summary>
		/// This method use to car list  by type.
		/// <summary>
		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateInput(true)]
		public ActionResult GetVehicles(int Type)
		{
			var d = new SelectList(_objAll.GetAll().ToList().FindAll(c => c.Type == Type).Where(c => c.IsActive == true).Select(c => new
			{
				Value = c.CarId,
				Text = c.CarName
			}), "Value", "Text");
			return Json(d, JsonRequestBehavior.AllowGet);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateInput(true)]
		public ActionResult GetRentalPlan(long CarId)
		{
			var d = new SelectList(_objallCR.GetAll().ToList().FindAll(c => c.CarId == CarId).Where(c => c.IsActive == true).Select(c => new
			{
				Value = c.RentalPlanId,
				Text = c.RentalPlan.RentalPlanName + "(£" + c.Price + ")"
			}), "Value", "Text");
			return Json(d, JsonRequestBehavior.AllowGet);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateInput(true)]
		public ActionResult GetDays(long RentalPlanId)
		{
			var data = 0;
			try
			{
				data = _objRP.FinById(RentalPlanId).NoOfDays;
			}
			catch
			{
			}
			return Json(data, JsonRequestBehavior.AllowGet);
		}


		public ActionResult Booking(long Id = 0)
		{
			var _objC = _objAll.FinById(Id);
			var _objV = new BookingViewModel();
			_objV.Type = _objC.Type;
			_objV.CarId = Id;

			ViewBag.Cars = new SelectList(_objAll.GetAll().ToList().FindAll(c => c.Type == (_objC != null ? _objC.Type : 0)).Where(c => c.IsActive == true).Select(c => new
			{
				Value = c.CarId,
				Text = c.CarName,
			}), "Value", "Text");

			ViewBag.RentalPlans = new SelectList(_objallCR.GetAll().ToList().FindAll(c => c.CarId == (_objC != null ? _objC.CarId : 0)).Where(c => c.IsActive == true).Select(c => new
			{
				Value = c.RentalPlanId,
				Text = c.RentalPlan.RentalPlanName + "(£" + c.Price + ")"
			}), "Value", "Text");
			return View(_objV);
		}

		[HttpPost]
		[ValidateInput(true)]
		[HandleError]
		public ActionResult Booking(BookingViewModel Model)
		{
			try
			{
				ViewBag.Cars = new SelectList(_objAll.GetAll().ToList().FindAll(c => c.Type == (Model != null ? Model.Type : 0)).Where(c => c.IsActive == true).Select(c => new
				{
					Value = c.CarId,
					Text = c.CarName
				}), "Value", "Text");

				ViewBag.RentalPlans = new SelectList(_objallCR.GetAll().ToList().FindAll(c => c.CarId == (Model != null ? Model.CarId : 0)).Where(c => c.IsActive == true).Select(c => new
				{
					Value = c.RentalPlanId,
					Text = c.RentalPlan.RentalPlanName + "(£" + c.Price + ")"
				}), "Value", "Text");
				if (ModelState.IsValid)
				{
					Booking obj = _objAllB.FinById(Model.BookingId);
					obj.BookingId = Model.BookingId;
					obj.CarId = Model.CarId;
					obj.RentalPlanId = Model.RentalPlanId;
					obj.Email = Model.Email;
					obj.Type = Model.Type;
					obj.IsActive = true;
					obj.EntryBy = 0;
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

					long chk = _objAllB.Save(obj);

					if (chk > 0)
					{
						ViewBag.Sucess = "Booking Detail saved successfully.";

						var carType = string.Empty;
						if (obj.Type != null)
						{
							carType = Enum.GetValues(typeof(CarType))
						   .Cast<CarType>()
						   .ToDictionary(k => (int)k, v => ((Enum)v).GetDescription()).Where(x => x.Key == obj.Type).Select(x => x.Value.ToString()).First();
						}
						var startDepot = string.Empty;
						if (obj.StartDeopt != null)
						{
							startDepot = Enum.GetValues(typeof(Depot))
						   .Cast<Depot>()
						   .ToDictionary(k => (int)k, v => ((Enum)v).GetDescription()).Where(x => x.Key == obj.StartDeopt).Select(x => x.Value.ToString()).First();
						}


						var returnDepot = string.Empty;
						if (obj.ReturnDeopt != null)
						{
							returnDepot = Enum.GetValues(typeof(Depot))
						.Cast<Depot>()
						.ToDictionary(k => (int)k, v => ((Enum)v).GetDescription()).Where(x => x.Key == obj.ReturnDeopt).Select(x => x.Value.ToString()).First();
						}

						var startTime = string.Empty;
						if (obj.StartTime != null)
						{
							startTime = Enum.GetValues(typeof(TimeSlots))
						   .Cast<TimeSlots>()
						   .ToDictionary(k => (int)k, v => ((Enum)v).GetDescription()).Where(x => x.Key == Convert.ToInt32(obj.StartTime)).Select(x => x.Value.ToString()).First();
						}


						var returnTime = string.Empty;
						if (obj.ReturnTime != null)
						{
							returnTime = Enum.GetValues(typeof(TimeSlots))
						.Cast<TimeSlots>()
						.ToDictionary(k => (int)k, v => ((Enum)v).GetDescription()).Where(x => x.Key == Convert.ToInt32(obj.ReturnTime)).Select(x => x.Value.ToString()).First();
						}

						var admin = _allAdmin.Find();
						var carName = _objAll.FinById(Model.CarId);
						var plandetail = _objallCR.GetAll().Where(c => c.RentalPlanId == Model.RentalPlanId && c.CarId == Model.CarId).FirstOrDefault();
						string content = TemplatesContent.BookingTemplate("Below are the booking Details", Model.Name, "", "http://www.example.com", "", carType, carName.CarName + "(" + carName.ModelName + ")", plandetail.RentalPlan.RentalPlanName + "(£" + plandetail.Price + ")", Model.StartDate.ToString("MMM-dd-yyyy"), startTime, startDepot, Model.ReturnDate.ToString("MMM-dd-yyyy"), returnTime, returnDepot, Model.Name + " " + Model.SurName, Model.Email, "House No- " + Model.HouseNumber + ", Street- " + Model.Street + ", Town- " + Model.Town + ", PostCode- " + Model.PostCode, ", Home Phone No- " + Model.HomePhone);
						string contentadmin = TemplatesContent.BookingTemplate("Below are the booking Details", admin.UserName, "", "http://www.example.com", "", carType, carName.CarName + "(" + carName.ModelName + ")", plandetail.RentalPlan.RentalPlanName + "(£" + plandetail.Price + ")", Model.StartDate.ToString("MMM-dd-yyyy"), startTime, startDepot, Model.ReturnDate.ToString("MMM-dd-yyyy"), returnTime, returnDepot, Model.Name + " " + Model.SurName, Model.Email, "House No- " + Model.HouseNumber + ", Street- " + Model.Street + ", Town- " + Model.Town + ", PostCode- " + Model.PostCode, ", Home Phone No- " + Model.HomePhone);

						Task.Run(() =>
						{
							MailHelper.SendMailMessage(Model.Email, "", "", "Booking Details", content.ToString());
						});

						Task.Run(() =>
						{
							MailHelper.SendMailMessage(admin.EmailId, "", "", "Recent Booking Details", contentadmin.ToString());
						});

						string Message = obj.Name;
						return RedirectToAction("Thanks", "Vehicle", new { Message = Message });
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

			return this.Booking(Model.BookingId);
		}
		[ValidateInput(false)]
		public ActionResult Thanks(string Message)
		{
			ViewBag.Message = Message;
			return View();
		}
	}
}
