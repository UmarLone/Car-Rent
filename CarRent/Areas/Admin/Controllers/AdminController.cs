using Codes;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViewModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace CarRent.Areas.admin.Controllers
{
    public class adminController : DerivedController
    {
        AllAdminUser allAdminUser = new AllAdminUser();
        //
        // GET: /admin/admin/
        public ActionResult Index()
        {
            allAdminUser.CreateAdmin();
            return View();
        }


        [HttpPost]
        public ActionResult Index(AdminUserViewmodel Model)
        {
            ViewBag.Check = "0";
            try
            {
                ModelState.Remove("EmailId");
                if (ModelState.IsValid)
                {

                    var usrs = allAdminUser.FinByUser(Model.UserName.Trim(), Model.Password.Trim());
                    if (usrs != null)
                    {
                        SessionWrapper.Admin = usrs;
                        if (SessionWrapper.Admin.AdminID != 0)
                        {
                            CookieWrapper.AdminId = usrs.AdminID;
                            return RedirectToAction("Index", "dashboard");
                        }
                        else
                            return View();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password");
                        ViewBag.Check = "1";
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                    ViewBag.Check = "1";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(Model);
            }
        }

        [Authenticate]
        public ActionResult Profile()
        {
            var d = allAdminUser.FinById(Convert.ToInt64(SessionWrapper.Admin.AdminID));
            return View(new AdminUserViewmodel(d));
        }



        [Authenticate]
        [HttpPost]

        public ActionResult Profile(AdminUserViewmodel Model, HttpPostedFileBase ProfileImage)
        {

            var usrs = allAdminUser.FinById(Model.AdminID);


            try
            {
                if (ModelState.IsValid)
                {


                    usrs.UserName = Model.UserName;
                    usrs.EmailId = Model.EmailId;

                    if (ProfileImage != null && ProfileImage.ContentLength > 0)
                    {
                        Model.ProfilePhoto = new byte[ProfileImage.ContentLength];
                        ProfileImage.InputStream.Read(Model.ProfilePhoto, 0, ProfileImage.ContentLength);
                        usrs.ProfilePhoto = Model.ProfilePhoto;

                    }

                    else
                    {

                        string filePath = Server.MapPath(Url.Content("~/Content/GalleryPhoto/nologo.png"));

                        FileStream fS = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        byte[] DafaultPhoto = new byte[fS.Length];
                        fS.Read(DafaultPhoto, 0, (int)fS.Length);
                        fS.Close();


                        usrs.ProfilePhoto = DafaultPhoto;

                    }



                    allAdminUser.Update(usrs);


















                    if (allAdminUser.Update(usrs) > 0)
                    {
                        SessionWrapper.Admin = usrs;
                        if (SessionWrapper.Admin.AdminID != 0)
                        {
                            CookieWrapper.AdminId = usrs.AdminID;

                            ViewBag.Sucess = "Profile updated successfully.";
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Profile Updation failed!");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Profile Updation failed!");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(Model);
            }
            return View(new AdminUserViewmodel(usrs));
        }



        [Authenticate]
        public ActionResult ChangePassword()
        {
            return View(new UserChangePassword());
        }

        [Authenticate]
        [HttpPost]
        public ActionResult ChangePassword(UserChangePassword obj)
        {
            try
            {
                ViewBag.Check = "0";
                ViewBag.Sucess = "";
                if (ModelState.IsValid)
                {
                    ViewBag.Check = "0";
                    if (SessionWrapper.Admin != null)
                    {
                        if (SessionWrapper.Admin.Password != obj.Password)
                        {
                            ModelState.AddModelError(string.Empty, "Old Password is wrong");
                            ViewBag.Check = "1";
                            return View();
                        }
                        else
                        {

                            var usrs = allAdminUser.FinById(SessionWrapper.Admin.AdminID);
                            usrs.Password = obj.NewPassword;
                            allAdminUser.Update(usrs);
                            SessionWrapper.Admin = usrs;
                            ViewBag.Sucess = "Password changed successfully.";
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    ViewBag.Check = "1";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(obj);
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View(new ForgotPassword());
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword Model)
        {
            try
            {

                ViewBag.Success = "";
                if (ModelState.IsValid)
                {
                    if (Model.EmailId != null)
                    {

                        var d = allAdminUser.FinByEmailId(Model.EmailId.Trim().ToLower());
                        if (string.IsNullOrEmpty(d.EmailId))
                        {
                            ModelState.AddModelError(string.Empty, "Email is wrong");
                            ViewBag.Sucess = "Wrong Email";
                            return View();
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("Your Password is: <b>" + d.Password + "<b/>");
                            string content = TemplatesContent.CommonTemplate("PASSWORD RECOVERY", "", sb.ToString(), "http://www.jollygoodvanhire.co.uk/", "", "", "", "", "", "", "", "", "");

                            Task.Run(() =>
                            {
                                MailHelper.SendMailMessage(Model.EmailId, "", "", "Password Recovery", content);
                            });

                            ViewBag.Success = "Password has been sent to your email . Please check your email";
                            return RedirectToAction("Index", "Admin");



                        }
                    }
                    else
                       {
                        ModelState.AddModelError(string.Empty, "Email is wrong");
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(Model);
            }

        }


        public ActionResult DashBoard()
        {
            return View();
        }

        [Authenticate]
        public ActionResult Logout()
        {
            try
            {

                FormsAuthentication.SignOut();
                Session.Clear();  // This may not be needed -- but can't hurt
                Session.Abandon();

                // Clear authentication cookie
                //HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                //rFormsCookie.Expires = DateTime.Now.AddYears(-1);
                //Response.Cookies.Add(rFormsCookie);

                // Clear session cookie 
                HttpCookie rSessionCookie = new HttpCookie(".ASPXAUTH", "");
                rSessionCookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(rSessionCookie);

                // Invalidate the Cache on the Client Side
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                CookieWrapper.AdminId = 0;
                return RedirectToAction("Index", "admin");
            }
            catch
            {
                FormsAuthentication.SignOut();
                Session.Clear();  // This may not be needed -- but can't hurt
                Session.Abandon();

                // Clear authentication cookie
                //HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                //rFormsCookie.Expires = DateTime.Now.AddYears(-1);
                //Response.Cookies.Add(rFormsCookie);

                // Clear session cookie 
                HttpCookie rSessionCookie = new HttpCookie(".ASPXAUTH", "");
                rSessionCookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(rSessionCookie);

                // Invalidate the Cache on the Client Side
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                CookieWrapper.AdminId = 0;
                return RedirectToAction("Index", "admin");
            }

        }

    }
}
