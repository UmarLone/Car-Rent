using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Codes;
using ViewModel;
using Models;
using Repositories;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        AllUser _objAll = new AllUser();
        User _obj = new User();

        [Authenticate]
        public ActionResult Index(string Search = "")
        {
            long total=0;
            if (!string.IsNullOrEmpty(Search))
            {
                ViewBag.Search = Search;
            }
            var _userList = _objAll.FindByAll(out total, Search.ToLower().Trim());
            ViewBag.TotalPages = total;
            return View(_userList);
        }

        [Authenticate]
        public ActionResult Detail(long Id = 0)
        {
            var user = new UserViewModel();
            if (Id > 0)
            {
                _obj = _objAll.FinByUserID(Id);
                user = new UserViewModel(_obj);
            }
            return View(user);
        }

        [Authenticate]
        [HttpPost]
        public ActionResult Detail(UserViewModel Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _obj = _objAll.FinByUserID(Model.UserId);
                    _obj.EmailId = Model.EmailId;

                    _obj.EntryBy = SessionWrapper.Admin.AdminID;
                    _obj.EntryDate = DateTime.Now;
                    _obj.FName = Model.FName;
                    _obj.LName = Model.LName;
                    _obj.Password = Model.Password;
                    _obj.UserName = Model.UserName;
                    _obj.IsActive = Model.IsActive;

                    long chk = _objAll.Save(_obj);
                    if (chk == -1)
                    {
                        ModelState.AddModelError(string.Empty, "User name already exists!");
                        return View(Model);
                    }
                    else if (chk == -2)
                    {

                        ModelState.AddModelError(string.Empty, "User name already exists!");
                        return View(Model);
                    }
                    else if (chk > 0)
                    {
                        ViewBag.Sucess = "User data saved successfully.";
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
            return View(new UserViewModel());
        }

        [Authenticate]
        public ActionResult Delete(long Id = 0)
        {
            if (Id > 0)
                _objAll.Delete(Id);
            TempData["Msg"] = "Data deleted successfully.";
            return RedirectToAction("Index");
        }
        public ActionResult Reply(string Email, string Message)
        {
            long chk = 0;
            Message = Message.TruncateAt(500);
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Thanks for subscription.<br/>");
                sb.Append(Message);
                string content = TemplatesContent.CommonTemplate("USER SUBSCRIPTION", "", sb.ToString(), "http://www.jollygoodvanhire.co.uk/", "", "", "", "", "", "", "", "", "");
                 
                Task.Run(() =>
                {
                    MailHelper.SendMailMessage(Email, "", "", "Greetings from Jolly Good Van Hire", content);
                });
                chk = 1;

            }
            catch
            {
            }
            return Json(chk, JsonRequestBehavior.AllowGet);
        }

    }
}
