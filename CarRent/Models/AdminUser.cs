/*********************************************
Author Name:    Umar Farooq
Date:           03 Feb 2014, 10:40 PM
/********************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
	[Table("AdminUsers")]
	public class AdminUser
	{
		[Key()]
		public long AdminID { get; set; }
		public virtual string UserName { get; set; }
		public virtual string Password { get; set; }
		public virtual byte[] ProfilePhoto { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual bool IsDelete { get; set; }
		public virtual long EntryBy { get; set; }
		public virtual long UpdateBy { get; set; }
		public virtual DateTime EntryDate { get; set; }
		public virtual DateTime? UpdateDate { get; set; }
		public virtual string EmailId
		{
			get;
			set;
		}
		public AdminUser() : this(0, string.Empty, string.Empty,null, DateTime.Now, 0, null, 0, false, false, string.Empty)
		{
		}

		public AdminUser(
			long _AdminId,
			string _UserName,
			string _Password,
			byte[] _ProfilePhoto,
			DateTime _EntryDate,
			long _EntryBy,
			DateTime? _UpdateDate,
			long _UpdateBy,
			bool _IsActive,
			bool _IsDeleted,
			string _EmailId
			)
		{
			this.AdminID = _AdminId;
			this.UserName = _UserName;
			this.Password = _Password;
			this.ProfilePhoto = _ProfilePhoto;
			this.EntryDate = _EntryDate;
			this.EntryBy = _EntryBy;
			this.UpdateDate = _UpdateDate;
			this.UpdateBy = _UpdateBy;
			this.IsActive = _IsActive;
			this.IsDelete = _IsDeleted;
			this.EmailId = _EmailId;
		}
	}
}