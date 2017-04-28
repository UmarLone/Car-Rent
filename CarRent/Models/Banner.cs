/*********************************************
Author Name:    Umar Farooq
Date:           12 Jan 2014, 12:16 PM
Description:    This is the class to declare the properties for admin.
/********************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// Banner (Which will be shown on the home page) related class file 
    ///Creation date: ‎‎Tuesday, ‎August ‎13, ‎2013, Author Name  :   1347
    /// <summary>

    [Table("Banner")]
    public class Banner
    {

        [Key]
        public  virtual int         BannerId     { get; set; }
        public  virtual string      BannerPic    { get; set; }
        public  virtual string      BannerText   { get; set; }
        public  virtual string      BannerTitle  { get; set; }
        public virtual long EntryBy { get; set; }
        public virtual long UpdateBy { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }


        public Banner()
            : this(0, string.Empty, string.Empty, string.Empty, false, false, 0, 0, System.DateTime.Now, null)
        {
        }
        public Banner(
            int         _bannerId,
            string      _bannerPic,
            string      _bannerText,
            string      _bannerTitle,
            bool        _isActive,
            bool        _isDeleted,
            Int64       _insertBy,
            Int64       _updateBy,
            DateTime    _insertDate,
            DateTime?   _updateDate
        )
        {
            this.BannerId       =   _bannerId;
            this.BannerPic      =   _bannerPic;
            this.BannerText     =   _bannerText;
            this.BannerTitle    =   _bannerTitle ;
            this.IsActive       =   _isActive;
            this.IsDeleted      =   _isDeleted;
            this.EntryBy       =   _insertBy;
            this.UpdateBy       =   _updateBy;
            this.EntryDate     =   _insertDate;
            this.UpdateDate     =   _updateDate;

        }
    }
}