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
    public class CarController : Controller
    {
        AllCar _objAll = new AllCar();
        AllCarRentalPlan _objallCR = new AllCarRentalPlan();
        AllRentalPlan _objRP = new AllRentalPlan();

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
            var _objV = new CarViewModel(_obj);
            _objV.CarRentalPlans = _objallCR.FinByCarId(Id);
            _objV.RentalPlans = _objRP.GetAll().Where(c => c.IsActive == true);
            return View(_objV);
        }

        [Authenticate]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(CarViewModel Model, HttpPostedFileBase PhotoImageFile, IEnumerable<long> RentalPlanIds, IEnumerable<long> CarRentalPlanIds, IEnumerable<decimal> Prices)
        {
            try
            {
                Model.CarImage = PhotoImageFile;
                Model.RentalPlans = _objRP.GetAll().Where(c => c.IsActive == true);
                Model.CarRentalPlans = _objallCR.FinByCarId(Model.CarId);

                if (ModelState.IsValid)
                {
                    Car obj = _objAll.FinById(Model.CarId);
                    obj.CarId = Model.CarId;
                    obj.BrandName = Model.BrandName;
                    obj.ModelName = Model.ModelName;
                    obj.CarName = Model.CarName;
                    obj.Type = Model.Type;
                    obj.Summary = Model.Summary;
                    obj.IsActive = Model.IsActive;
                    obj.EntryBy = SessionWrapper.Admin.AdminID;
                    obj.EntryDate = DateTime.Now;
                    obj.WhyHire = Model.WhyHire;

                    if (Model.CarImage != null && Model.CarImage.ContentLength > 0)
                    {
                        string path = ConfigurationWrapper.GalleryPhoto;
                        Model.CarPhoto = Guid.NewGuid() + Path.GetExtension(Model.CarImage.FileName);

                        if (!System.IO.Directory.Exists(Server.MapPath(path)))
                            System.IO.Directory.CreateDirectory(Server.MapPath(path));

                        if (System.IO.File.Exists(Server.MapPath(path + Model.CarPhoto)))
                            System.IO.File.Delete(Server.MapPath(path + Model.CarPhoto));

                        Model.CarImage.SaveAs(Server.MapPath(path + Model.CarPhoto));
                        obj.CarPhoto = Model.CarPhoto;

                    }


                    long chk = _objAll.Save(obj);

                    if (chk > 0)
                    {
                        ViewBag.Sucess = "Vehicle Detail saved successfully.";
                        if (Prices != null)
                        {
                            for (int i = 0; i < RentalPlanIds.Count(); i++)
                            {
                                if (RentalPlanIds.ToList()[i] > 0)
                                {
                                    CarRentalPlan objC = new CarRentalPlan();
                                    objC.CarId = chk;
                                    objC.CarRentalPlanId = Convert.ToInt64(CarRentalPlanIds.ToList()[i]);
                                    objC.Price = Convert.ToDecimal(Prices.ToList()[i]);
                                    objC.EntryBy = SessionWrapper.Admin.AdminID;
                                    objC.RentalPlanId = Convert.ToInt64(RentalPlanIds.ToList()[i]);
                                    objC.IsActive = true;
                                    objC.IsDelete = false;
                                    objC.EntryDate = DateTime.Now;
                                    _objallCR.Save(objC);
                                }
                            }
                        }
                    }
                    else if (chk == -1)
                    {
                        ModelState.AddModelError(string.Empty, "Vehicle Name already exists.");
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

            return this.Detail(Model.CarId);
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
