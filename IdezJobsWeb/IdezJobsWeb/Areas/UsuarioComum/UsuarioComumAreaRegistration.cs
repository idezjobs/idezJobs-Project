using System.Web.Mvc;

namespace IdezJobsWeb.Areas.UsuarioComum
{
    public class UsuarioComumAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UsuarioComum";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UsuarioComum_default",
                "UsuarioComum/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
