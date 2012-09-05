using System.Web.Mvc;

namespace IdezJobsWeb.Areas.Administrativa
{
    public class AdministrativaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrativa";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administrativa_default",
                "Administrativa/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
