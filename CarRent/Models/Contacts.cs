/*********************************************
Author Name:   Umar Farooq
Date:           12 Jan 2014, 12:16 PM
Description:    This is the class to declare the properties for admin.
/********************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
	[Table("Contacts")]
	public class Contacts
	{
		[Key]
		public virtual long ContactsId { get; set; }
		public virtual string Name { get; set; }
		public virtual string EmailId { get; set; }
		public virtual string Suggestion { get; set; }
		public virtual DateTime EntryDate { get; set; }
		public virtual long EntryBy { get; set; }
		public virtual DateTime? UpdateDate { get; set; }
		public virtual long UpdateBy { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual bool IsDeleted { get; set; }
		public virtual string Subject { get; set; }
		public virtual string Phone { get; set; }
		//	public virtual int Location { get; set; }

		public Contacts()
			: this(0, string.Empty, string.Empty, string.Empty, DateTime.Now, 0, null, 0, false, false, string.Empty, string.Empty)
		{
		}

		public Contacts(
			long _ContactsId,
			string _Name,
			string _EmailId,
			string _Suggestion,
			DateTime _EntryDate,
			long _EntryBy,
			DateTime? _UpdateDate,
			long _UpdateBy,
			bool _IsActive,
			bool _IsDeleted,
			string _Subject,
			string _Phone
			//   int _Location
			)
		{
			this.ContactsId = _ContactsId;
			this.Suggestion = _Suggestion;
			this.EntryDate = _EntryDate;
			this.EntryBy = _EntryBy;
			this.UpdateDate = _UpdateDate;
			this.UpdateBy = _UpdateBy;
			this.IsActive = _IsActive;
			this.IsDeleted = _IsDeleted;
			this.EmailId = _EmailId;
			this.Name = _Name;
			this.Subject = _Subject;
			this.Phone = _Phone;
			//  this.Location = _Location;
		}
	}
}
