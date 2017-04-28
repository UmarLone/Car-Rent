/*********************************************
Author Name:    Umar Farooq
Date:           14 Jan 2014, 12:40 PM
Description:    This is the class to declare the properties for FinanceApps with validation.
/********************************************/

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel
{

	[NotMapped]
	public class ContactsViewModel : Contacts
	{
		public ContactsViewModel() { }
		public ContactsViewModel(Contacts obj)
		{
			this.ContactsId = obj.ContactsId;
			this.EntryBy = obj.EntryBy;
			this.EntryDate = obj.EntryDate;
			this.IsActive = obj.IsActive;
			this.IsDeleted = obj.IsDeleted;
			this.Suggestion = obj.Suggestion;
			this.UpdateBy = obj.UpdateBy;
			this.UpdateDate = obj.UpdateDate;
			this.Name = obj.Name;
			this.EmailId = obj.EmailId;
			this.Subject = obj.Subject;
			this.Phone = obj.Phone;
			// this.Location = obj.Location;
		}

		[Required(ErrorMessage = "Name is required.")]
		[MaxLength(100, ErrorMessage = "Name should be less the 100 characters")]
		public override string Name
		{
			get;
			set;
		}


		[Required(ErrorMessage = "Email id is required!")]
		[EmailAddress(ErrorMessage = "Email id is not valid!")]
		[MaxLength(200, ErrorMessage = "Email id not be more than 200 characters!")]
		public override string EmailId
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Message is required!")]
		[MaxLength(500, ErrorMessage = "Message not be more than 500 characters!")]
		public override string Suggestion { get; set; }

		[Required(ErrorMessage = "Subject is required!")]
		[MaxLength(200, ErrorMessage = "Subject not be more than 200 characters!")]
		public override string Subject { get; set; }


		[Required(ErrorMessage = "Phone is required!")]
		[MaxLength(20, ErrorMessage = "Phone not be more than 20 characters!")]
		public override string Phone { get; set; }
		/*
        [Required(ErrorMessage = "Please select Location!")]
        public override int Location { get; set; }
*/
		public StaticContentViewModel StaticContent { get; set; }
	}
}