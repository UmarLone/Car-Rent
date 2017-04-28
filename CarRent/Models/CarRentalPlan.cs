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
    [Table("CarRentalPlan")]
    public class CarRentalPlan
    {
        [Key]
        public virtual  long        CarRentalPlanId     { get; set; }
        public virtual  long        CarId               { get; set; }
        public virtual  long        RentalPlanId        { get; set; }
        public virtual  bool        IsActive            { get; set; }
        public virtual  bool        IsDelete            { get; set; }
        public virtual  long        EntryBy             { get; set; }
        public virtual  long        UpdateBy            { get; set; }
        public virtual  DateTime    EntryDate           { get; set; }
        public virtual  DateTime?   UpdateDate          { get; set; }
        public virtual decimal Price { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; }

        [ForeignKey("RentalPlanId")]
        public RentalPlan RentalPlan { get; set; }

        public CarRentalPlan()
            : this(0, 0, 0, false, false, 0, 0, DateTime.Now, null, 0)
        {}

        public CarRentalPlan(
            long        _CarRentalPlanId ,
            long        _CarId           ,
            long        _RentalPlanId    ,
            bool        _IsActive        ,
            bool        _IsDelete        ,
            long        _EntryBy         ,
            long        _UpdateBy        ,
            DateTime    _EntryDate       ,
            DateTime?   _UpdateDate    ,
            decimal _Price
            )
        {
            this.CarRentalPlanId    =  _CarRentalPlanId ;
            this.CarId              =  _CarId           ;
            this.RentalPlanId       =  _RentalPlanId    ;
            this.IsActive           =  _IsActive        ;
            this.IsDelete           =  _IsDelete        ;
            this.EntryBy            =  _EntryBy         ;
            this.UpdateBy           =  _UpdateBy        ;
            this.EntryDate          =  _EntryDate       ;
            this.UpdateDate         =  _UpdateDate      ;
            this.Price              =  _Price           ;
        }

    }

    
}