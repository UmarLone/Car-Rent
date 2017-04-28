using System.Web.Mvc;

namespace CarRent.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "Admin_default",
               "Admin/{controller}/{action}/{id}",
               new { action = "Index", controller = "Admin", id = UrlParameter.Optional },
               new string[] { "CarRent.Areas.Admin.Controllers" }
           );
        }
    }
}
