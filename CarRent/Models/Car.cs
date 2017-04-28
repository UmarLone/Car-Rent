/*********************************************
Author Name:  Umar Farooq
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
    [Table("Car")]
    public class Car
    {
        [Key]
        public  virtual     long        CarId       { get; set; }
        public  virtual     string      CarName     { get; set; }
        public  virtual     string      BrandName   { get; set; }
        public  virtual     string      ModelName   { get; set; }
        public  virtual     string      Summary     { get; set; }
        public  virtual     string      WhyHire     { get; set; }
        public  virtual     bool        IsActive    { get; set; }
        public  virtual     bool        IsDelete    { get; set; }
        public  virtual     long        EntryBy     { get; set; }
        public  virtual     long        UpdateBy    { get; set; }
        public  virtual     DateTime    EntryDate   { get; set; }
        public  virtual     DateTime?   UpdateDate  { get; set; }
        public  virtual     int         Type        { get; set; }
        public virtual string CarPhoto { get; set; }
        public Car()
            : this(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, 0, 0, DateTime.Now, null, 0, string.Empty)
        { }

        public Car(
            long        _CarId      ,
            string      _CarName,
            string      _BrandName  ,
            string      _ModelName  ,
            string      _Summary    ,
            string      _WhyHire    ,
            bool        _IsActive   ,
            bool        _IsDelete   ,
            long        _EntryBy    ,
            long        _UpdateBy   ,
            DateTime    _EntryDate  ,
            DateTime?   _UpdateDate ,
            int         _Type,
            string _CarPhoto
            )
        {
            this.CarId          =   _CarId      ;
            this.CarName        =   _CarName;
            this.BrandName      =   _BrandName  ;
            this.ModelName      =   _ModelName  ;
            this.Summary        =   _Summary    ;
            this.WhyHire        =   _WhyHire    ;
            this.IsActive       =   _IsActive   ;
            this.IsDelete       =   _IsDelete   ;
            this.EntryBy        =   _EntryBy    ;
            this.UpdateBy       =   _UpdateBy   ;
            this.EntryDate      =   _EntryDate  ;
            this.UpdateDate     =   _UpdateDate ;
            this.Type           =   _Type;
            this.CarPhoto = _CarPhoto;
        }
    }
}