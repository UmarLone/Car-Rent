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
	public class CarRentalPlanViewModel : CarRentalPlan
	{
		public CarRentalPlanViewModel() { }

		public CarRentalPlanViewModel(CarRentalPlan obj)
		{
			this.CarRentalPlanId = obj.CarRentalPlanId;
			this.CarId = obj.CarId;
			this.RentalPlanId = obj.RentalPlanId;
			this.IsActive = obj.IsActive;
			this.IsDelete = obj.IsDelete;
			this.EntryBy = obj.EntryBy;
			this.UpdateBy = obj.UpdateBy;
			this.EntryDate = obj.EntryDate;
			this.UpdateDate = obj.UpdateDate;
		}
	}
}