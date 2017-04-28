/*********************************************
Author Name:   Umar Farooq
Date:           12 Jan 2014, 12:16 PM
Description:    This is the class which manage all database related functionality for News.
/********************************************/

using Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
    public class AllBanner
    {
        Repository db = new Repository();
        protected readonly int pageSize = ConfigurationWrapper.PageSize;
        protected readonly int page;

        public AllBanner()
        {
            page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
        }
        /// <summary>
        /// This method use to save or update data.
        /// <summary>
        public long Save(Banner u)
        {
            if (u.BannerId != 0)
            {
                u.UpdateDate = DateTime.Now;
                db.Banner.Update(c => c = u);
            }

            else
            {

                 u = db.Banner.Add(new Banner(
                 u.BannerId,
                 u.BannerPic,
                 u.BannerText,
                 u.BannerTitle,
                 u.IsActive,
                 u.IsDeleted,
                 u.EntryBy,
                 u.UpdateBy,
                 u.EntryDate,
                 u.UpdateDate
                    ));
            }
            db.SaveChanges();

            return u.BannerId;
        }
        /// <summary>
        /// This method use to find all data which are not deleted with searchable and pagging capibilities.
        /// <summary>
        public IEnumerable<Banner> FindByAll(out long total, string search = "")
        {
            var d = db.Banner.Where(u => u.IsDeleted == false);
            if (!string.IsNullOrWhiteSpace(search))
                d = d.Where(c => c.BannerText.ToLower().Contains(search) || c.BannerTitle.ToLower().Contains(search));
            total = d.Count();
            d = d.OrderByDescending(x => x.BannerId).Page(pageSize, page);
            return d;
        }
        /// <summary>
        /// This method use to find particular data by uniqueid.
        /// <summary>
        public Banner FindById(int BannerId)
        {
            var d = db.Banner.FirstOrDefault(c => c.BannerId == BannerId);
            if (d != null)
                return d;
            else
                return new Banner();
        }

        public void Delete(long? ID)
        {
            var banner = db.Banner.Where(d => d.BannerId == ID).FirstOrDefault();
            banner.UpdateDate = DateTime.Now;
            banner.IsDeleted = true;
            banner.IsActive = false;
            db.Banner.Update(c => c = banner);
            db.SaveChanges();
        }
        /// <summary>
        /// This method use to find all data which are not deleted.
        /// <summary>
        public IEnumerable<Banner> GetAllBanners()
        {
            return db.Banner.Where(u => u.IsDeleted == false);
        }
        /// <summary>
        /// This method use to find all data which are active.
        /// <summary>
        public IEnumerable<Banner> GetBanners()
        {
            return db.Banner.Where(u => u.IsActive == true);
        }
    }
}