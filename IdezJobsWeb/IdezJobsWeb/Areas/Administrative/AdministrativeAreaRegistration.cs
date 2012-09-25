using System.Web.Mvc;

namespace IdezJobsWeb.Areas.Administrative
{
    public class AdministrativeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrative";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administrative_default",
                "Administrative/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "IdezJobsWeb.Areas.Administrative.Controllers" }
            );
        }
    }
}
