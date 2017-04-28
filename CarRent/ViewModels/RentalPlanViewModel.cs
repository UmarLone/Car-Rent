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
	public class RentalPlanViewModel : RentalPlan
	{
		public RentalPlanViewModel() { }

		public RentalPlanViewModel(RentalPlan obj)
		{
			this.RentalPlanId = obj.RentalPlanId;
			this.RentalPlanName = obj.RentalPlanName;
			this.Summary = obj.Summary;
			this.IsActive = obj.IsActive;
			this.IsDelete = obj.IsDelete;
			this.EntryBy = obj.EntryBy;
			this.UpdateBy = obj.UpdateBy;
			this.EntryDate = obj.EntryDate;
			this.UpdateDate = obj.UpdateDate;
			this.NoOfDays = obj.NoOfDays;
		}

		[Required(ErrorMessage = "Rental plan name is required!")]
		public override string RentalPlanName { get; set; }
		[Required(ErrorMessage = "NUmber of days is required!")]
		public override int NoOfDays { get; set; }
		[Required(ErrorMessage = "Summary is required!")]
		public override string Summary { get; set; }
	}
}