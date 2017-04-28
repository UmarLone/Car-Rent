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
	[Table("Booking")]
	public class Booking
	{
		[Key]
		public virtual long BookingId { get; set; }
		public virtual long CarId { get; set; }
		public virtual long RentalPlanId { get; set; }
		public virtual string Name { get; set; }
		public virtual string SurName { get; set; }
		public virtual string Email { get; set; }
		public virtual string HomePhone { get; set; }

		public virtual string HouseNumber { get; set; }
		public virtual string Street { get; set; }
		public virtual string Town { get; set; }
		public virtual string PostCode { get; set; }
		public virtual DateTime StartDate { get; set; }
		public virtual string StartTime { get; set; }
		public virtual int StartDeopt { get; set; }
		public virtual DateTime ReturnDate { get; set; }
		public virtual string ReturnTime { get; set; }
		public virtual int ReturnDeopt { get; set; }
		public virtual int Status { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual bool IsDelete { get; set; }
		public virtual long EntryBy { get; set; }
		public virtual long UpdateBy { get; set; }
		public virtual DateTime EntryDate { get; set; }
		public virtual DateTime? UpdateDate { get; set; }
		public virtual int Type { get; set; }

		[ForeignKey("CarId")]
		public Car Car { get; set; }

		[ForeignKey("RentalPlanId")]
		public RentalPlan RentalPlan { get; set; }

		public Booking()
			: this(0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, string.Empty, 0, DateTime.Now, string.Empty, 0, 0, false, false, 0, 0, DateTime.Now, null, 0)
		{ }

		public Booking(
			long _BookingId,
			long _CarId,
			long _RentalPlanId,
			string _Name,
			string _SurName,
			string _Email,
			string _HomePhone,

			string _HouseNumber,
			string _Street,
			string _Town,
			string _PostCode,
			DateTime _StartDate,
			string _StartTime,
			int _StartDeopt,
			DateTime _ReturnDate,
			string _ReturnTime,
			int _ReturnDeopt,
			int _Status,
			bool _IsActive,
			bool _IsDelete,
			long _EntryBy,
			long _UpdateBy,
			DateTime _EntryDate,
			DateTime? _UpdateDate,
			int _Type
			)
		{

			this.BookingId = _BookingId;
			this.CarId = _CarId;
			this.RentalPlanId = _RentalPlanId;
			this.Name = _Name;
			this.SurName = _SurName;
			this.Email = _Email;
			this.HomePhone = _HomePhone;

			this.HouseNumber = _HouseNumber;
			this.Street = _Street;
			this.Town = _Town;
			this.PostCode = _PostCode;
			this.StartDate = _StartDate;
			this.StartTime = _StartTime;
			this.StartDeopt = _StartDeopt;
			this.ReturnDate = _ReturnDate;
			this.ReturnTime = _ReturnTime;
			this.ReturnDeopt = _ReturnDeopt;
			this.Status = _Status;
			this.IsActive = _IsActive;
			this.IsDelete = _IsDelete;
			this.EntryBy = _EntryBy;
			this.UpdateBy = _UpdateBy;
			this.EntryDate = _EntryDate;
			this.UpdateDate = _UpdateDate;
			this.Type = _Type;

		}
	}
}