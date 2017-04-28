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
    public class AllCar
    {
        protected readonly int pageSize = ConfigurationWrapper.PageSize;
        protected readonly int page;
        public AllCar()
        {
            page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
        }
        Repository db = new Repository();

        public long Save(Car m)
        {
            long chk = 0;
            if (IsNameExists(m.CarName, m.CarId))
            {
                chk = -1;
                return chk;
            }

            if (m.CarId > 0)
            {
                Update(m);
            }
            else
            {
                m = db.Car.Add(new Car(
                                        m.CarId,
                                        m.CarName,
                                        m.BrandName,
                                        m.ModelName,
                                        m.Summary,
                                        m.WhyHire,
                                        m.IsActive,
                                        m.IsDelete,
                                        m.EntryBy,
                                        m.UpdateBy,
                                        m.EntryDate,
                                        m.UpdateDate,
                                        m.Type,
                                        m.CarPhoto
                                       )
                           );
            }
            db.SaveChanges();
            chk = m.CarId;
            return chk;
        }

        public bool IsNameExists(string CarName, long CarId)
        {
            bool chk = false;
            if (CarId > 0)
            {
                if (db.Car.Any(c => c.CarName == CarName && c.CarId != CarId && c.IsDelete == false))
                    chk = true;
                else
                    chk = false;
            }
            else
            {
                if (db.Car.Any(c => c.CarName == CarName && c.IsDelete == false))
                    chk = true;
                else
                    chk = false;
            }
            return chk;
        }
        public IEnumerable<Car> FindByAll(out long TotalPages, string searchString)
        {
            var d = db.Car.Where(c => c.IsDelete == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                d = d.Where(c => c.CarName.ToLower().Contains(searchString.ToLower()) || c.BrandName.ToLower().Contains(searchString.ToLower()) || c.ModelName.ToLower().Contains(searchString.ToLower()));
            }
            TotalPages = d.Count();
            d = d.OrderByDescending(x => x.CarId);
            return d.Page(pageSize, page);

        }
        public IEnumerable<Car> FindByAllForFront(out long TotalPages, string searchString, int type = 0)
        {
            var d = db.Car.Where(c => c.IsActive == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                d = d.Where(c => c.CarName.ToLower().Contains(searchString.ToLower()) || c.BrandName.ToLower().Contains(searchString.ToLower()) || c.ModelName.ToLower().Contains(searchString.ToLower()));
            }
            if (type > 0)
                d = d.Where(c => c.Type == type);

            TotalPages = d.Count();
            d = d.OrderByDescending(x => x.CarId);
            return d.Page(pageSize, page);

        }
        public Car FinById(long CarId)
        {
            var data = db.Car.Where(d => d.CarId == CarId).FirstOrDefault();
            if (data == null)
                data = new Car();
            return data;

        }
        public void Delete(long? ID)
        {
            var Admin = db.Car.Where(d => d.CarId == ID).FirstOrDefault();
            Admin.UpdateDate = DateTime.Now;
            Admin.IsDelete = true;
            Admin.IsActive = false;
            db.Car.Update(c => c = Admin);
            db.SaveChanges();
        }
        public long Update(Car m)
        {
            if (m.CarId > 0)
            {
                m.UpdateBy = m.CarId;
                m.UpdateDate = DateTime.Now;
                db.Car.Update(c => c = m);
            }
            db.SaveChanges();
            return m.CarId;
        }
        public string GetPlanNameByCarId(long CarId)
        {
            AllRentalPlan _objRP = new AllRentalPlan();
            AllCarRentalPlan _objallCR = new AllCarRentalPlan();
            string PlanName = "";
            var CR = _objallCR.FinByCarId(CarId);
            var planId = _objallCR.FinByCarId(CarId).Min(c => c.RentalPlanId);
            var perAmount = _objallCR.GetAll().Where(c => c.RentalPlanId == planId && c.CarId == CarId).FirstOrDefault();
            var obj = _objRP.FinById(planId);
            PlanName = "£" + perAmount.Price + " per " + _objRP.FinById(planId).NoOfDays + " day(s)";
            return PlanName;
        }
        public IEnumerable<Car> GetAll()
        {
            var d = db.Car.Where(c => c.IsDelete == false);
            return d;
        }
    }
}