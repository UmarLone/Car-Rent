/*********************************************
Author Name:    Umar Farooq
Date:           12 Jan 2014, 12:16 PM
Description:    This is the class which manage all database related functionality for User.
/********************************************/

using Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
    public class AllUser
    {
        protected readonly int pageSize = ConfigurationWrapper.PageSize;
        protected readonly int page;
        Repository db = new Repository();
        public AllUser()
        {
            page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
        }
        public long Save(User m)
        {
            long chk = 0;
            if (IsNameExists(m.UserName, m.UserId))
            {
                chk = -1;
                return chk;
            }
            if (IsEmailExists(m.EmailId, m.UserId))
            {
                chk = -2;
                return chk;
            }

            if (m.UserId != 0)
            {
                m.UpdateBy = m.UserId;
                m.UpdateDate = DateTime.Now;
                db.User.Update(c => c = m);
                chk = m.UserId;
            }
            else
            {
                m = db.User.Add(new User(
                                        m.UserId,
                                        m.UserName,
                                        m.EmailId,
                                        m.Password,
                                        m.FName,
                                        m.LName,
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
            chk = m.UserId;
            return chk;
        }
        public void Delete(long? ID)
        {
            var User = db.User.Where(d => d.UserId == ID).FirstOrDefault();
            User.UpdateDate = DateTime.Now;
            User.IsDeleted = true;
            User.IsActive = false;
            db.User.Update(c => c = User);
            db.SaveChanges();
        }

        public bool IsNameExists(string UserName, long UserId)
        {
            bool chk = false;
            if (UserId > 0)
            {
                if (db.User.Any(c => c.UserName.Trim().ToLower() == UserName.Trim().ToLower() && c.UserId != UserId && c.IsDeleted == false))
                    chk = true;
                else
                    chk = false;
            }
            else
            {
                if (db.User.Any(c => c.UserName.Trim().ToLower() == UserName.Trim().ToLower() && c.IsDeleted == false))
                    chk = true;
                else
                    chk = false;
            }
            return chk;
        }

        public bool IsEmailExists(string EmailId, long UserId)
        {
            bool chk = false;
            if (UserId > 0)
            {
                if (db.User.Any(c => c.EmailId.Trim().ToLower() == EmailId.Trim().ToLower() && c.UserId != UserId && c.IsDeleted == false))
                    chk = true;
                else
                    chk = false;
            }
            else
            {
                if (db.User.Any(c => c.EmailId.Trim().ToLower() == EmailId.Trim().ToLower() && c.IsDeleted == false))
                    chk = true;
                else
                    chk = false;
            }
            return chk;
        }

        public IEnumerable<User> FindByAll(out long TotalPages, string searchString)
        {
            var d = db.User.Where(c => c.IsDeleted == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                d = d.Where(c => c.UserName.Trim().ToLower().Contains(searchString.Trim().ToLower()) || c.EmailId.Trim().ToLower().Contains(searchString.Trim().ToLower()) || (c.FName + " " + c.LName).Trim().ToLower().Contains(searchString.Trim().ToLower()));
            }
            TotalPages = d.Count();
            d = d.OrderByDescending(x => x.UserId);
            return d.Page(pageSize, page);

        }
        public User FinByUserID(long? ID)
        {
            var User = db.User.Where(d => d.UserId == ID).FirstOrDefault();
            if (User == null)
                User = new User();
            return User;

        }

        public User FinByEmailID(string EmailId)
        {
            var User = db.User.Where(d => d.EmailId.ToLower().Trim() == EmailId).FirstOrDefault();
            if (User == null)
                User = new User();
            return User;

        }

        public IEnumerable<User> GetAll()
        {
            var d = db.User.Where(c => c.IsDeleted == false);
            return d;
        }

        public long CheckLogin(string UserName, string Password)
        {
            long UserId = 0;
            try
            {
                if (db.User.Any(c => (c.UserName == UserName || c.EmailId == UserName) && c.Password == Password))
                    UserId = db.User.Where(c => (c.UserName == UserName || c.EmailId == UserName) && c.Password == Password).FirstOrDefault().UserId;
            }
            catch
            {
            }
            return UserId;
        }
    }
}