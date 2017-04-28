/*********************************************
Author Name:    Umar Farooq
Date:           12 Jan 2014, 12:16 PM
Description:    This is the class to declare the properties for admin.
/********************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Models;
using System.Web.Mvc;
using Codes;
using System.ComponentModel.DataAnnotations.Schema;
using Repositories;

namespace ViewModel
{
	/// <summary>
	/// Banner (maintence the Banner details) related class file with overriden some property validation
	/// Creation date: Tuesday, ‎August ‎13, ‎2013, Author Name  :   1347
	/// <summary>

	[NotMapped]
	public class BannerViewModel : Banner
	{
		[Codes.RegexExpression.ImageExpression(ErrorMessage = "Image is not valid")]
		public HttpPostedFileBase BannerImage { get; set; }

		public BannerViewModel()
		{
		}

		public BannerViewModel(Banner u)
		{
			this.BannerId = u.BannerId;
			this.BannerPic = u.BannerPic;
			this.BannerText = u.BannerText;
			this.BannerTitle = u.BannerTitle;
			this.IsActive = u.IsActive;
			this.IsDeleted = u.IsDeleted;
			this.EntryBy = u.EntryBy;
			this.UpdateBy = u.UpdateBy;
			this.EntryDate = u.EntryDate;
			this.UpdateDate = u.UpdateDate;
		}
	}
}