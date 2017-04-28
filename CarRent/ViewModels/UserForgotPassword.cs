using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel
{
    [NotMapped]
    public class UserForgotPassword
    {
        [Required(ErrorMessage = "Plese Enter Your User ID")]
        public string UserName { get; set; }
        
    }
}