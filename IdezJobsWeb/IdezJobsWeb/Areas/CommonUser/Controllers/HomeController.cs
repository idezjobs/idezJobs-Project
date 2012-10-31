using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IdezJobsWeb.Areas.CommonUser.Controllers
{
	[Authorize(Roles = "Student")]
	[HandleError(View="Error")]
	public class HomeController : Controller
    {
       

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Sucess( )
		{
			return View( );
		}

		public ActionResult LogOff( )
		{
			FormsAuthentication.SignOut( );

			return RedirectToAction("Index", "Home");
		}

    }
}
