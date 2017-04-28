/*********************************************
Author Name:   Umar Farooq
Date:           12 Jan 2014, 12:16 PM
Description:    This is the class which manage all database related functionality for StaticContent.
/********************************************/

using Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
    public class AllStaticContent
    {
        protected readonly int pageSize = ConfigurationWrapper.PageSize;
        protected readonly int page;
        Repository db = new Repository();
        public AllStaticContent()
        {
            page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
        }
        public long Save(StaticContent m)
        {
            long chk = 0;
            if (IsNameExists(m.ContentType, m.StaticContentId))
            {
                chk = -1;
                return chk;
            }
            if (m.StaticContentId != 0)
            {
                m.UpdateBy = m.StaticContentId;
                m.UpdateDate = DateTime.Now;
                db.StaticContent.Update(c => c = m);
                chk = m.StaticContentId;
            }
            else
            {
                m = db.StaticContent.Add(new StaticContent(
                                        m.StaticContentId,
                                        m.ContentType,
                                        m.Description,
                                        m.EntryDate,
                                        m.EntryBy,
                                        m.UpdateDate,
                                        m.UpdateBy,
                                        m.IsActive,
                                        m.IsDeleted
                                       )
                           );
            }
            db.SaveChanges();
            chk = m.StaticContentId;
            return chk;
        }
        public void Delete(long? ID)
        {
            var StaticContent = db.StaticContent.Where(d => d.StaticContentId == ID).FirstOrDefault();
            StaticContent.UpdateDate = DateTime.Now;
            StaticContent.IsDeleted = true;
            StaticContent.IsActive = false;
            db.StaticContent.Update(c => c = StaticContent);
            db.SaveChanges();
        }

        public bool IsNameExists(int ContentType, long StaticContentId)
        {
            bool chk = false;
            if (StaticContentId > 0)
            {
                if (db.StaticContent.Any(c => c.ContentType == ContentType && c.StaticContentId != StaticContentId && c.IsDeleted == false))
                    chk = true;
                else
                    chk = false;
            }
            else
            {
                if (db.StaticContent.Any(c => c.ContentType == ContentType && c.IsDeleted == false))
                    chk = true;
                else
                    chk = false;
            }
            return chk;
        }

        public IEnumerable<StaticContent> FindByAll(out long TotalPages, string searchString)
        {
            var d = db.StaticContent.Where(c => c.IsDeleted == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                d = d.Where(c => c.Description.Trim().ToLower().Contains(searchString.Trim().ToLower()));
            }
            TotalPages = d.Count();
            d = d.OrderByDescending(x => x.StaticContentId);
            return d.Page(pageSize, page);

        }
        public StaticContent FinByStaticContentID(long? ID)
        {
            var staticContent = db.StaticContent.FirstOrDefault(d => d.StaticContentId == ID);
            if (staticContent == null)
                staticContent = new StaticContent();
            return staticContent;

        }

        public StaticContent FinByStaticContentType(int type)
        {
            var staticContent = db.StaticContent.FirstOrDefault(d => d.ContentType == type) ?? new StaticContent();
                
               // db.StaticContent.Where(d => d.ContentType == Type).FirstOrDefault();
            return staticContent;

        }

        public IEnumerable<StaticContent> GetAll()
        {
            var d = db.StaticContent.Where(c => c.IsDeleted == false);
            return d;
        }

    }
}