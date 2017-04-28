/*********************************************
Author Name:    Umar Farooq
Date:           03 Feb 2014, 10:40 PM
/********************************************/

using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ViewModel
{
	[NotMapped]
	public class AdminUserViewmodel : AdminUser
	{
        [Codes.RegexExpression.ImageExpression(ErrorMessage = "Image is not valid")]
        public HttpPostedFileBase ProfileImage { get; set; }
        public AdminUserViewmodel() { }

		public AdminUserViewmodel(AdminUser obj)
		{
			this.EmailId = obj.EmailId;
			this.AdminID = obj.AdminID;
			this.ProfilePhoto = obj.ProfilePhoto;
			this.EntryBy = obj.EntryBy;
			this.EntryDate = obj.EntryDate;
			this.IsActive = obj.IsActive;
			this.IsDelete = obj.IsDelete;
			this.Password = obj.Password;
			this.UpdateBy = obj.UpdateBy;
			this.UpdateDate = obj.UpdateDate;
			this.UserName = obj.UserName;
		}


		[Required(ErrorMessage = "Please Enter User Name")]
		public override string UserName { get; set; }
		[Required(ErrorMessage = "Please Enter Password")]
		public override string Password { get; set; }
		public override byte[] ProfilePhoto { get; set; }
		[Required(ErrorMessage = "Email id is required!")]
		[EmailAddress(ErrorMessage = "Email id is not valid!")]
		[MaxLength(200, ErrorMessage = "Email id not be more than 200 characters!")]
		public override string EmailId
		{
			get;
			set;
		}








	}
}