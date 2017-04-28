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
    [Table("User")]
    public class User
    {
        [Key]
        public virtual long UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string EmailId { get; set; }
        public virtual string Password { get; set; }
        public virtual string FName { get; set; }
        public virtual string LName { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual long EntryBy { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual long UpdateBy { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }

        public User()
            : this(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, 0, null, 0, false, false)
        {
        }

        public User(
            long _UserId,
            string _UserName,
            string _EmailId,
            string _Password,
            string _FName,
            string _LName,
            DateTime _EntryDate,
            long _EntryBy,
            DateTime? _UpdateDate,
            long _UpdateBy,
            bool _IsActive,
            bool _IsDeleted
            )
        {
            this.UserId = _UserId;
            this.UserName = _UserName;
            this.EmailId = _EmailId;
            this.Password = _Password;
            this.FName = _FName;
            this.LName = _LName;
            this.EntryDate = _EntryDate;
            this.EntryBy = _EntryBy;
            this.UpdateDate = _UpdateDate;
            this.UpdateBy = _UpdateBy;
            this.IsActive = _IsActive;
            this.IsDeleted = _IsDeleted;
        }
    }
}