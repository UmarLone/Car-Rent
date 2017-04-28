/*********************************************
Author Name:  Umar Farooq
Date:           03 Feb 2014, 10:40 PM
/********************************************/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Codes;
using ViewModel;
using Models;
using Repositories;

namespace CarRent.Areas.admin.Controllers
{
    public class DashboardController : DerivedController
    {

        AllBooking _objAll = new AllBooking();
        DashBoardChart obj = new DashBoardChart();

        [Authenticate]
        public ActionResult Index()
        {
            var datalist = _objAll.GetAll().Where(c => c.IsActive == true);

            obj.CarJan = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Jan).Count();
            obj.CarFeb = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Feb).Count();
            obj.CarMar = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Mar).Count();
            obj.CarApr = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Apr).Count();
            obj.CarMay = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.May).Count();
            obj.CarJune = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Jun).Count();
            obj.CarJuly = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Jul).Count();
            obj.CarAug = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Aug).Count();
            obj.CarSept = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Sep).Count();
            obj.CarOct = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Oct).Count();
            obj.CarNov = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Nov).Count();
            obj.CarDec = datalist.Where(c => c.Type == (int)CarType.Car && c.StartDate.Month == (int)Months.Dec).Count();

            obj.VanJan = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Jan).Count();
            obj.VanFeb = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Feb).Count();
            obj.VanMar = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Mar).Count();
            obj.VanApr = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Apr).Count();
            obj.VanMay = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.May).Count();
            obj.VanJune = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Jun).Count();
            obj.VanJuly = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Jul).Count();
            obj.VanAug = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Aug).Count();
            obj.VanSept = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Sep).Count();
            obj.VanOct = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Oct).Count();
            obj.VanNov = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Nov).Count();
            obj.VanDec = datalist.Where(c => c.Type == (int)CarType.Van && c.StartDate.Month == (int)Months.Dec).Count();

            obj.AvgJan = (Convert.ToDecimal(obj.CarJan) + Convert.ToDecimal(obj.VanJan)) / 2;
            obj.AvgFeb = (Convert.ToDecimal(obj.CarFeb) + Convert.ToDecimal(obj.VanFeb)) / 2;
            obj.AvgMar = (Convert.ToDecimal(obj.CarMar) + Convert.ToDecimal(obj.VanMar)) / 2;
            obj.AvgApr = (Convert.ToDecimal(obj.CarApr) + Convert.ToDecimal(obj.VanApr)) / 2;
            obj.AvgMay = (Convert.ToDecimal(obj.CarMay) + Convert.ToDecimal(obj.VanMay)) / 2;
            obj.AvgJune = (Convert.ToDecimal(obj.CarJune) + Convert.ToDecimal(obj.VanJune)) / 2;
            obj.AvgJuly = (Convert.ToDecimal(obj.CarJuly) + Convert.ToDecimal(obj.VanJuly)) / 2;
            obj.AvgAug = (Convert.ToDecimal(obj.CarAug) + Convert.ToDecimal(obj.VanAug)) / 2;
            obj.AvgSept = (Convert.ToDecimal(obj.CarSept) + Convert.ToDecimal(obj.VanSept)) / 2;
            obj.AvgOct = (Convert.ToDecimal(obj.CarOct) + Convert.ToDecimal(obj.VanOct)) / 2;
            obj.AvgNov = (Convert.ToDecimal(obj.CarNov) + Convert.ToDecimal(obj.VanNov)) / 2;
            obj.AvgDec = (Convert.ToDecimal(obj.CarDec) + Convert.ToDecimal(obj.VanDec)) / 2;

            obj.TotalCar =
                obj.CarJan +
                obj.CarFeb +
                obj.CarMar +
                obj.CarApr +
                obj.CarMay +
                obj.CarJune +
                obj.CarJuly +
                obj.CarAug +
                obj.CarSept +
                obj.CarOct +
                obj.CarNov +
                obj.CarDec;


            obj.TotalVan =
                obj.VanJan +
                obj.VanFeb +
                obj.VanMar +
                obj.VanApr +
                obj.VanMay +
                obj.VanJune +
                obj.VanJuly +
                obj.VanAug +
                obj.VanSept +
                obj.VanOct +
                obj.VanNov +
                obj.VanDec;

            return View(obj);
        }

    }
}

