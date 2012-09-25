using System.Web.Mvc;

namespace IdezJobsWeb.Areas.CommonUser
{
    public class CommonUserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CommonUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CommonUser_default",
                "CommonUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 new string[] { "IdezJobsWeb.Areas.CommonUser.Controllers" }
            );
        }
    }
}
