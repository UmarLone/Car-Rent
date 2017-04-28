/*********************************************
Author Name:  Umar Farooq
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

namespace CarRent.Areas.Admin.Controllers
{
    public class RentalPlanController : Controller
    {
        AllRentalPlan _objAll = new AllRentalPlan();

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

        [Authenticate]
        public ActionResult Detail(long Id = 0)
        {
            var _obj = _objAll.FinById(Id);
            return View(new RentalPlanViewModel(_obj));
        }

        [Authenticate]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(RentalPlanViewModel Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RentalPlan obj = _objAll.FinById(Model.RentalPlanId);
                    obj.RentalPlanId = Model.RentalPlanId;
                    obj.RentalPlanName = Model.RentalPlanName;
                    obj.Summary = Model.Summary;
                    obj.IsActive = Model.IsActive;
                    obj.EntryBy = SessionWrapper.Admin.AdminID;
                    obj.EntryDate = DateTime.Now;
                    obj.NoOfDays = Model.NoOfDays;
                    long chk = _objAll.Save(obj);

                    if (chk > 0)
                    {
                        ViewBag.Sucess = "Rental Plan saved successfully.";
                    }
                    else if (chk == -1)
                    {
                        ModelState.AddModelError(string.Empty, "Plan Name already exists.");
                        return View(Model);

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
            return this.Detail(Model.RentalPlanId);
        }

        [Authenticate]
        public ActionResult Delete(long Id = 0)
        {
            TempData["Message"] = "";
            try
            {
                _objAll.Delete(Id);
                TempData["Message"] = "Plan deleted successfully.";
            }
            catch
            {
                TempData["Message"] = "Plan deletion failed!";
            }
            return RedirectToAction("Index");
        }
    }
}
