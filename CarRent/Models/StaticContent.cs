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
    [Table("StaticContent")]
    public class StaticContent
    {
        [Key]
        public virtual long StaticContentId { get; set; }
        public virtual int ContentType { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual long EntryBy { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual long UpdateBy { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }

        public StaticContent()
            : this(0, 0, string.Empty, DateTime.Now, 0, null, 0, false, false)
        {
        }

        public StaticContent(
            long _StaticContentId,
            int _ContentType,
            string _Description,
            DateTime _EntryDate,
            long _EntryBy,
            DateTime? _UpdateDate,
            long _UpdateBy,
            bool _IsActive,
            bool _IsDeleted
            )
        {
            this.StaticContentId = _StaticContentId;
            this.ContentType = _ContentType;
            this.Description = _Description;
            this.EntryDate = _EntryDate;
            this.EntryBy = _EntryBy;
            this.UpdateDate = _UpdateDate;
            this.UpdateBy = _UpdateBy;
            this.IsActive = _IsActive;
            this.IsDeleted = _IsDeleted;
        }
    }
}