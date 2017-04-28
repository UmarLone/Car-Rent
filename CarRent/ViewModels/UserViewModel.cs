/*********************************************
Author Name:    Umar Farooq
Date:           14 Jan 2014, 12:40 PM
Description:    This is the class to declare the properties for admin with validation.
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
	public class UserViewModel : User
	{
		public UserViewModel() { }

		public UserViewModel(User obj)
		{
			this.UserId = obj.UserId;
			this.UserName = obj.UserName;
			this.EmailId = obj.EmailId;
			this.Password = obj.Password;
			this.FName = obj.FName;
			this.LName = obj.LName;
			this.EntryDate = obj.EntryDate;
			this.EntryBy = obj.EntryBy;
			this.UpdateDate = obj.UpdateDate;
			this.UpdateBy = obj.UpdateBy;
			this.IsActive = obj.IsActive;
			this.IsDeleted = obj.IsDeleted;
		}


		//[Required(ErrorMessage = "Enter User Name")]
		//[RegularExpression(@"^[a-zA-Z0-9\S]+$", ErrorMessage = "Invalid username only characters A-Z, a-z and digits are  acceptable.")]
		//[MaxLength(20, ErrorMessage = "User Name should be less the 20 characters")]
		//[Remote("IsNameExists", "User", AdditionalFields = "UserId", ErrorMessage = "User name already exist.")]
		//public override string UserName { get; set; }

		//[Required(ErrorMessage = "Password is required.")]
		//[MaxLength(20, ErrorMessage = "Password should be less the 20 characters")]
		//public override string Password
		//{
		//    get;
		//    set;
		//}

		//[Required(ErrorMessage = "Confirm Password is required.")]
		//[MaxLength(20, ErrorMessage = "Confirm Password should be less the 20 characters")]
		//[System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		//public string ConfirmPassword
		//{
		//    get;
		//    set;
		//}

		//[Required(ErrorMessage = "First Name is required.")]
		//[MaxLength(100, ErrorMessage = "Frist Name should be less the 100 characters")]
		//public override string FName
		//{
		//    get;
		//    set;
		//}

		//[Required(ErrorMessage = "Last Name is required.")]
		//[MaxLength(100, ErrorMessage = "Last Name should be less the 100 characters")]
		//public override string LName
		//{
		//    get;
		//    set;
		//}

		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		[Required(ErrorMessage = "Email id is required.")]
		[MaxLength(200, ErrorMessage = "Email id not be more than 300 characters")]
		public override string EmailId
		{
			get;
			set;
		}
	}
}