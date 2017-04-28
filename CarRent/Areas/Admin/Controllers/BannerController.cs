/*********************************************
Author Name:   Umar Farooq
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
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace CarRent.Areas.Admin.Controllers
{
    public class BannerController : DerivedController
    {
        AllBanner _allObj = new AllBanner();
        /// <summary>
        /// This method use to open the list page of banners.
        /// <summary>
        [Authenticate]
        public ActionResult Index(string Search = "")
        {
            if (!string.IsNullOrEmpty(Search))
                ViewBag.Search = Search;

            long total = 0;
            var cat = _allObj.FindByAll(out total, Search.Trim().ToLower());
            ViewBag.TotalPages = total;
            return View(cat);
        }
        /// <summary>
        /// This method use to open the banner detail page to add update.
        /// <summary>
        [Authenticate]
        public ActionResult Detail(int Id = 0)
        {
            ViewBag.Sucess = "";
            if (Id > 0)
            {
                var a = _allObj.FindById(Id);
                return View(new BannerViewModel(a));
            }
            return View(new BannerViewModel());
        }
        /// <summary>
        /// This method use to open the banner detail page to add update.
        /// <summary>
        [Authenticate]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(BannerViewModel _ecom, HttpPostedFileBase BannerImage)
        {
            _ecom.EntryDate = System.DateTime.Now;
            _ecom.EntryBy = SessionWrapper.Admin.AdminID;
            _ecom.BannerImage = BannerImage;
            if (ModelState.IsValid)
            {
                try
                {
                    Banner _ec = _allObj.FindById(_ecom.BannerId);
                    _ec.BannerId = _ecom.BannerId;
                    _ec.BannerText = _ecom.BannerText;
                    _ec.BannerTitle = _ecom.BannerTitle;
                    _ec.EntryDate = _ecom.EntryDate;
                    _ec.EntryDate = _ecom.EntryDate;
                    _ec.IsActive = _ecom.IsActive;
                    _ec.IsDeleted = false;

                    if (_ecom.BannerImage != null && _ecom.BannerImage.ContentLength > 0)
                    {
                        string path = ConfigurationWrapper.GalleryPhoto;
                        _ecom.BannerPic = Guid.NewGuid() + Path.GetExtension(_ecom.BannerImage.FileName);
                        if (!System.IO.Directory.Exists(Server.MapPath(path)))
                            System.IO.Directory.CreateDirectory(Server.MapPath(path));
                        if (System.IO.File.Exists(Server.MapPath(path + _ecom.BannerPic)))
                            System.IO.File.Delete(Server.MapPath(path + _ecom.BannerPic));
                        _ecom.BannerImage.SaveAs(Server.MapPath(path + _ecom.BannerPic));
                        _ec.BannerPic = _ecom.BannerPic;

                        //System.Drawing.Image _img = System.Drawing.Image.FromFile(Server.MapPath(path + _ecom.BannerPic));
                        //if (_img.Width > 822 || _img.Height > 180)
                        //{
                        //    ModelState.AddModelError(string.Empty, "Banner size should be not more than 800x120 (widthxheight)");
                        //    return View(_ecom);
                        //}

                    }

                    var _catId = _allObj.Save(_ec);
                    if (_catId > 0)
                    {
                        ViewBag.Sucess = "Banner saved successfully.";

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Banner submission failed");
                        return View(_ecom);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View(_ecom);
                }
            }
            else
            {
                return View(_ecom);
            }
            return View(new BannerViewModel());

        }

        /// <summary>
        /// This method use to delete particular banner.
        /// <summary>
        [Authenticate]
        public ActionResult Delete(int Id = 0)
        {
            if (Id > 0)
                _allObj.Delete(Id);
            TempData["Msg"] = "Data deleted successfully.";
            return RedirectToAction("Index");
        }


    }
}
