/*********************************************
Author Name:    Umar Farooq
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

namespace CarRent.Areas.Admin.Controllers
{
	public class ContactUsController : Controller
	{
		AllContacts _objAll = new AllContacts();
		Contacts _obj = new Contacts();

		[Authenticate]
		public ActionResult Index(string Search = "")
		{
			long total = 0;
			if (!string.IsNullOrEmpty(Search))
			{
				ViewBag.Search = Search;
			}

			var _ContactsList = _objAll.FindByAll(out total, Search.ToLower().Trim());
			ViewBag.TotalPages = total;
			return View(_ContactsList);
		}

		[Authenticate]
		public ActionResult Detail(long Id = 0)
		{
			var Contacts = new ContactsViewModel();
			if (Id > 0)
			{
				_obj = _objAll.FinByContactsID(Id);
				Contacts = new ContactsViewModel(_obj);
			}
			return View(Contacts);
		}


		[Authenticate]
		[HttpPost]
		public ActionResult Detail(ContactsViewModel Model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_obj = _objAll.FinByContactsID(Model.ContactsId);
					_obj.EmailId = Model.EmailId;
					_obj.EntryBy = SessionWrapper.Admin.AdminID;
					_obj.EntryDate = DateTime.Now;
					_obj.Name = Model.Name;
					_obj.Suggestion = Model.Suggestion;
					_obj.IsActive = Model.IsActive;
					_obj.Subject = Model.Subject;
					_obj.Phone = Model.Phone;
					// _obj.Location = Model.Location;

					long chk = _objAll.Save(_obj);
					if (chk > 0)
					{
						ViewBag.Sucess = "Contact saved successfully.";
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
			return View(new ContactsViewModel());
		}
		[Authenticate]
		public ActionResult Delete(long Id = 0)
		{
			if (Id > 0)
				_objAll.Delete(Id);
			TempData["Msg"] = "Data deleted successfully.";
			return RedirectToAction("Index");
		}

		public ActionResult Reply(string Name, string Email, string Message)
		{
			long chk = 0;
			Message = Message.TruncateAt(500);
			try
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("We have received your message. Below is our response.<br/>");
				sb.Append(Message);
				string content = TemplatesContent.CommonTemplate("USER ENQUERY", "", sb.ToString(), "http://www.jollygoodvanhire.co.uk/", "", "", "", "", "", "", "", "", "");

				Task.Run(() =>
				{
					MailHelper.SendMailMessage(Email, "", "", "Reply From Jolly Good Van Hire in response to your enquiry", content);
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
