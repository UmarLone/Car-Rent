/*********************************************
Author Name:   Umar Farooq
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
	public class BookingViewModel : Booking
	{
		public BookingViewModel() { }

		public BookingViewModel(Booking obj)
		{
			this.BookingId = obj.BookingId;
			this.CarId = obj.CarId;
			this.RentalPlanId = obj.RentalPlanId;
			this.Name = obj.Name;
			this.SurName = obj.SurName;
			this.Email = obj.Email;
			this.HomePhone = obj.HomePhone;

			this.HouseNumber = obj.HouseNumber;
			this.Street = obj.Street;
			this.Town = obj.Town;
			this.PostCode = obj.PostCode;
			this.StartDate = obj.StartDate;
			this.StartTime = obj.StartTime;
			this.StartDeopt = obj.StartDeopt;
			this.ReturnDate = obj.ReturnDate;
			this.ReturnTime = obj.ReturnTime;
			this.ReturnDeopt = obj.ReturnDeopt;
			this.Status = obj.Status;
			this.IsActive = obj.IsActive;
			this.IsDelete = obj.IsDelete;
			this.EntryBy = obj.EntryBy;
			this.UpdateBy = obj.UpdateBy;
			this.EntryDate = obj.EntryDate;
			this.UpdateDate = obj.UpdateDate;
			this.Type = obj.Type;
			this.Car = obj.Car;
			this.RentalPlan = obj.RentalPlan;
		}

		[Required(ErrorMessage = "Please select car!")]
		public override long CarId { get; set; }
		[Required(ErrorMessage = "Please select Rental Plan!")]
		public override long RentalPlanId { get; set; }
		[Required(ErrorMessage = "Please enter Name!")]
		public override string Name { get; set; }
		[Required(ErrorMessage = "Please enter Sur Name!")]
		public override string SurName { get; set; }
		[Required(ErrorMessage = "Please enter Email!")]
		[EmailAddress(ErrorMessage = "Invalid Email Id!")]
		public override string Email { get; set; }
		[Required(ErrorMessage = "Please enter Home Phone!")]
		public override string HomePhone { get; set; }

		[Required(ErrorMessage = "Please enter House Number!")]
		public override string HouseNumber { get; set; }
		[Required(ErrorMessage = "Please enter Street!")]
		public override string Street { get; set; }
		[Required(ErrorMessage = "Please enter Town!")]
		public override string Town { get; set; }
		[Required(ErrorMessage = "Please enter Postal Code!")]
		public override string PostCode { get; set; }
		[Required(ErrorMessage = "Please enter Start Date!")]
		public override DateTime StartDate { get; set; }
		[Required(ErrorMessage = "Please select Start Time!")]
		public override string StartTime { get; set; }
		[Required(ErrorMessage = "Please select Start Deopt!")]
		public override int StartDeopt { get; set; }
		[Required(ErrorMessage = "Please select Return Date!")]
		public override DateTime ReturnDate { get; set; }
		[Required(ErrorMessage = "Please select Return Time!")]
		public override string ReturnTime { get; set; }
		[Required(ErrorMessage = "Please select Return Deopt!")]
		public override int ReturnDeopt { get; set; }

		public IEnumerable<Car> Cars { get; set; }
	}
}