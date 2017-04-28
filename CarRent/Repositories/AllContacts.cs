/*********************************************
Author Name:    Umar Farooq
Date:           12 Jan 2014, 12:16 PM
Description:    This is the class which manage all database related functionality for Contacts.
/********************************************/

using Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
	public class AllContacts
	{
		protected readonly int pageSize = ConfigurationWrapper.PageSize;
		protected readonly int page;
		Repository db = new Repository();

		public AllContacts()
		{
			page = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"] ?? "1");
		}
		public long Save(Contacts m)
		{
			long chk = 0;

			m = db.Contacts.Add(new Contacts(
									m.ContactsId,
									m.Name,
									m.EmailId,
									m.Suggestion,
									m.EntryDate,
									m.EntryBy,
									m.UpdateDate,
									m.UpdateBy,
									m.IsActive,
									m.IsDeleted,
									m.Subject,
									m.Phone
								   // m.Location
								   )
					   );

			db.SaveChanges();
			chk = m.ContactsId;
			return chk;
		}
		public void Delete(long? ID)
		{
			var Contacts = db.Contacts.Where(d => d.ContactsId == ID).FirstOrDefault();
			Contacts.UpdateDate = DateTime.Now;
			Contacts.IsDeleted = true;
			Contacts.IsActive = false;
			db.Contacts.Update(c => c = Contacts);
			db.SaveChanges();
		}


		public IEnumerable<Contacts> FindByAll(out long TotalPages, string searchString)
		{
			var d = db.Contacts.Where(c => c.IsDeleted == false);
			if (!string.IsNullOrEmpty(searchString))
			{
				d = d.Where(c => c.Name.Trim().ToLower().Contains(searchString.Trim().ToLower()) || c.Suggestion.Trim().ToLower().Contains(searchString.Trim().ToLower()) || c.EmailId.Trim().ToLower().Contains(searchString.Trim().ToLower()));
			}

			TotalPages = d.Count();
			d = d.OrderByDescending(x => x.ContactsId);
			return d.Page(pageSize, page);

		}


		public Contacts FinByContactsID(long? ID)
		{
			var Contacts = db.Contacts.Where(d => d.ContactsId == ID).FirstOrDefault();
			if (Contacts == null)
				Contacts = new Contacts();
			return Contacts;

		}

		public IEnumerable<Contacts> GetAll()
		{
			var d = db.Contacts.Where(c => c.IsDeleted == false);
			return d;
		}

	}
}