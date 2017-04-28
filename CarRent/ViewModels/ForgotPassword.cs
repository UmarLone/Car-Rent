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
	public class ForgotPassword
	{
		[Required(ErrorMessage = "Email id is required!")]
		[EmailAddress(ErrorMessage = "Email id is not valid!")]
		[MaxLength(200, ErrorMessage = "Email id not be more than 200 characters!")]
		public string EmailId
		{
			get;
			set;
		}
	}
}