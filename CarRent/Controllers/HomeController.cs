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
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Controllers
{
	public class HomeController : DerivedController
	{
		AllStaticContent _objAll = new AllStaticContent();
		StaticContent _obj = new StaticContent();
		AllContacts _objAllC = new AllContacts();
		Contacts _objC = new Contacts();
		ContactsViewModel _objCV = new ContactsViewModel();
		AllAdminUser _allAdmin = new AllAdminUser();

		public ActionResult Index()
		{
			// redirecting desktop/detection users
			/*  if (Request.Browser["IsMobileDevice"] != "true")
			   {
				  Response.Redirect("http://www.jollygoodvanhire.co.uk/");
			   }*/
			_obj = new StaticContentViewModel(_objAll.FinByStaticContentType((int)ContentType.Home));
			return View(_obj);
		}

		public ActionResult About()
		{
			_obj = new StaticContentViewModel(_objAll.FinByStaticContentType((int)ContentType.AboutUs));
			return View(_obj);
		}

		public ActionResult Blog()
		{




			return View();


		}

		public ActionResult Contact()
		{
			_objCV.StaticContent = new StaticContentViewModel(_objAll.FinByStaticContentType((int)ContentType.ContactUs));
			return View(_objCV);
		}
		public ActionResult EuropeanVanHire()
		{
			_obj = new StaticContentViewModel(_objAll.FinByStaticContentType((int)ContentType.EuropeanVanHire));
			return View(_obj);
		}



		public ActionResult MonthlyRental()
		{
			_obj = new StaticContentViewModel(_objAll.FinByStaticContentType((int)ContentType.MonthlyRental));
			return View(_obj);


		}







		[HttpPost]
		[ValidateInput(true)]

		public ActionResult Contact(ContactsViewModel Model)
		{
			Model.StaticContent = new StaticContentViewModel(_objAll.FinByStaticContentType((int)ContentType.ContactUs));

			try
			{

				if (ModelState.IsValid)
				{
					_objC = _objAllC.FinByContactsID(Model.ContactsId);
					_objC.EmailId = Model.EmailId;
					_objC.EntryBy = 0;
					_objC.EntryDate = DateTime.Now;
					_objC.Name = Model.Name;
					_objC.Suggestion = Model.Suggestion;
					_objC.Subject = Model.Subject;
					_objC.Phone = Model.Phone;
					//_objC.Location = Model.Location;
					_objC.IsActive = true;
					_objC.Subject = Model.Subject;
					_objC.Phone = Model.Phone;
					//	_objC.Location = Model.Location;

					long chk = _objAllC.Save(_objC);
					if (chk > 0)
					{
						Model.ContactsId = chk;
						ViewBag.Sucess = "You Enquiry sent successfully.";

						StringBuilder sb = new StringBuilder();
						var admin = _allAdmin.Find();
						sb.Append(Model.Suggestion);
						string content = TemplatesContent.CommonTemplate("USER ENQUERY", "", sb.ToString(), "http://www.example.com", "", "", "", "", "", "", "", "", "");

						Task.Run(() =>
						{
							MailHelper.SendMailMessage(admin.EmailId, "", "", Model.Subject, content);
						});

						string Message = _objC.Name;

						return RedirectToAction("Thanks", "Vehicle", new { Message = Message });
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Your enquiry submission failed!");
					return View(Model);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return View(Model);
			}
			return this.Contact();
		}
		public ActionResult TermPolicy()
		{
			_obj = new StaticContentViewModel(_objAll.FinByStaticContentType((int)ContentType.TermsofService));
			return View(_obj);
		}





		public ActionResult Reply(string Email)
		{
			long chk = 0;
			try
			{
				AllUser _allu = new AllUser();
				var admin = _allAdmin.Find();
				User _obj = new User();
				_obj = _allu.FinByEmailID(Email);

				_obj.EmailId = Email;
				_obj.EntryBy = 0;
				_obj.EntryDate = DateTime.Now;
				_obj.FName = "N/A";
				_obj.LName = "N/A";
				_obj.Password = Email;
				_obj.UserName = Email;
				_obj.IsActive = true;
				if (_allu.Save(_obj) > 0)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("Thanks for subscription.<br/>");
					sb.Append("<br>We will approach with our special offers shortly.<br/>");
					string content1 = TemplatesContent.CommonTemplate("SUBSCRIPTION FOR OFFERES AND NEWS", "", sb.ToString(), "http://www.jollygoodvanhire.co.uk/", "", "", "", "", "", "", "", "", "");

					Task.Run(() =>
					{
						MailHelper.SendMailMessage(Email, "", "", "Greetings from Jolly Good  Hire", content1);
					});
					chk = 1;

					var d = _allAdmin.Find();
					StringBuilder sb1 = new StringBuilder();
					sb1.Append("<br>User " + Email + " has been subscribed!.<br/>You can also manage from admin panel to share with offers and news. <br/>");

					string content = TemplatesContent.CommonTemplate("USER SUBSCRIPTION DETAILS", "", sb1.ToString(), "http://www.example.com/", "", "", "", "", "", "", "", "", "");

					Task.Run(() =>
					{
						MailHelper.SendMailMessage(admin.EmailId, "", "", "Subscription Details", content);
					});
					chk = 1;
				}

			}

			catch
			{
			}
			return Json(chk, JsonRequestBehavior.AllowGet);
		}

	}

}
