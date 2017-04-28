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
	public class CarViewModel : Car
	{
		public CarViewModel() { }

		public CarViewModel(Car obj)
		{
			this.CarId = obj.CarId;
			this.CarName = obj.CarName;
			this.BrandName = obj.BrandName;
			this.ModelName = obj.ModelName;
			this.Summary = obj.Summary;
			this.WhyHire = obj.WhyHire;
			this.IsActive = obj.IsActive;
			this.IsDelete = obj.IsDelete;
			this.EntryBy = obj.EntryBy;
			this.UpdateBy = obj.UpdateBy;
			this.EntryDate = obj.EntryDate;
			this.UpdateDate = obj.UpdateDate;
			this.Type = obj.Type;
			this.CarPhoto = obj.CarPhoto;

		}

		[Required(ErrorMessage = "Car Name is required!")]
		public override string CarName { get; set; }
		[Required(ErrorMessage = "Brand Name is required!")]
		public override string BrandName { get; set; }
		[Required(ErrorMessage = "Model Name is required!")]
		public override string ModelName { get; set; }
		[Required(ErrorMessage = "Summary is required!")]
		public override string Summary { get; set; }
		[Required(ErrorMessage = "Why Hire is required!")]
		public override string WhyHire { get; set; }
		[Required(ErrorMessage = "Type is required!")]
		public override int Type { get; set; }
		public IEnumerable<Car> Cars { get; set; }
		public IEnumerable<CarRentalPlan> CarRentalPlans { get; set; }
		public IEnumerable<RentalPlan> RentalPlans { get; set; }
		public IEnumerable<long> RentalPlanIds { get; set; }
		public IEnumerable<long> CarRentalPlanIds { get; set; }
		public IEnumerable<long> PlanId { get; set; }
		public IEnumerable<decimal> Prices { get; set; }
		[Codes.RegexExpression.ImageExpression(ErrorMessage = "Invalid image file!")]
		public HttpPostedFileBase CarImage { get; set; }
		public string PlanName { get; set; }
	}
}