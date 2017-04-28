/*********************************************
Author Name: Umar Farooq
Date:           14 Jan 2014, 12:40 PM
Description:    This is the class to declare the properties for admin with validation.
/********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using Models;
using Repositories;
using Codes;
using System.IO;

namespace CarRent.Areas.Admin.Controllers
{
    public class StaticContentController : DerivedController
    {
        AllStaticContent _objAll = new AllStaticContent();
        StaticContent _obj = new StaticContent();

        [Authenticate]
        public ActionResult Index(string Search = "")
        {
            long total = 0;
            if (!string.IsNullOrEmpty(Search))
            {
                ViewBag.Search = Search;
            }
            var _StaticContentList = _objAll.FindByAll(out total, Search.ToLower().Trim());
            ViewBag.TotalPages = total;
            return View(_StaticContentList);
        }

        [Authenticate]
        public ActionResult Detail(long Id = 0)
        {
            var StaticContent = new StaticContentViewModel();
            if (Id > 0)
            {
                _obj = _objAll.FinByStaticContentID(Id);
                StaticContent = new StaticContentViewModel(_obj);
            }
            return View(StaticContent);
        }

        [Authenticate]
        [HttpPost, ValidateInput(false)]
        public ActionResult Detail(StaticContentViewModel Model, HttpPostedFileBase ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _obj = _objAll.FinByStaticContentID(Model.StaticContentId);
                    _obj.Description = Model.Description;
                    _obj.EntryBy = SessionWrapper.Admin.AdminID;
                    _obj.EntryDate = DateTime.Now;
                    _obj.ContentType = Model.ContentType;
                    _obj.IsActive = Model.IsActive;

                    long chk = _objAll.Save(_obj);
                    if (chk == -1)
                    {
                        ModelState.AddModelError(string.Empty, "Content for this type already exists!");
                        return View(Model);
                    }
                    else if (chk > 0)
                    {
                        ViewBag.Sucess = "Content data saved successfully.";
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
            return View(new StaticContentViewModel());
        }

        [Authenticate]
        public ActionResult Delete(long Id = 0)
        {
            if (Id > 0)
                _objAll.Delete(Id);
            TempData["Msg"] = "Data deleted successfully.";
            return RedirectToAction("Index");
        }

    }
}
