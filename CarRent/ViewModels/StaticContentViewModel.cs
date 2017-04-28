/*********************************************
Author Name:   Umar Farooq
Date:           14 Jan 2014, 12:40 PM
Description:    This is the class to declare the properties for StaticContent with validation.
/********************************************/

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;


namespace ViewModel
{
    [NotMapped]
    public class StaticContentViewModel : StaticContent
    {
        public StaticContentViewModel() { }

        public StaticContentViewModel(StaticContent obj)
        {
            this.StaticContentId = obj.StaticContentId;
            this.ContentType = obj.ContentType;
            this.Description = obj.Description;
            this.EntryDate = obj.EntryDate;
            this.EntryBy = obj.EntryBy;
            this.UpdateDate = obj.UpdateDate;
            this.UpdateBy = obj.UpdateBy;
            this.IsActive = obj.IsActive;
            this.IsDeleted = obj.IsDeleted;
        }

        [Required(ErrorMessage = "Select Content Type")]
        public override int ContentType { get; set; }

        public override string Description { get; set; }
        public ContactsViewModel Contacts { get; set; }
    }
}