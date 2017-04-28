/*********************************************
Author Name:   Umar Farooq
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
    [Table("RentalPlan")]
    public class RentalPlan
    {
        [Key]
        public  virtual     long        RentalPlanId       { get; set; }
        public  virtual     string      RentalPlanName     { get; set; }
        public  virtual     string      Summary            { get; set; }
        public  virtual     bool        IsActive           { get; set; }
        public  virtual     bool        IsDelete           { get; set; }
        public  virtual     long        EntryBy            { get; set; }
        public  virtual     long        UpdateBy           { get; set; }
        public  virtual     DateTime    EntryDate          { get; set; }
        public  virtual     DateTime?   UpdateDate         { get; set; }
        public  virtual     int NoOfDays { get; set; }

        public RentalPlan()
            : this(0, string.Empty, string.Empty, false, false, 0, 0, DateTime.Now, null,0)
        { }

        public RentalPlan(
            long        _RentalPlanId      ,
            string      _RentalPlanName,
            string      _Summary    ,
            bool        _IsActive   ,
            bool        _IsDelete   ,
            long        _EntryBy    ,
            long        _UpdateBy   ,
            DateTime    _EntryDate  ,
            DateTime?   _UpdateDate ,
            int         _NoOfDays
            )
        {
            this.RentalPlanId          =   _RentalPlanId        ;
            this.RentalPlanName        =   _RentalPlanName      ;
            this.Summary               =   _Summary             ;
            this.IsActive              =   _IsActive            ;
            this.IsDelete              =   _IsDelete            ;
            this.EntryBy               =   _EntryBy             ;
            this.UpdateBy              =   _UpdateBy            ;
            this.EntryDate             =   _EntryDate           ;
            this.UpdateDate            =   _UpdateDate          ;
            this.NoOfDays              =   _NoOfDays;
        }
    }
}