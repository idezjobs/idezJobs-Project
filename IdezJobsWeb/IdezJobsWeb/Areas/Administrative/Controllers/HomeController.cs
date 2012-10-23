using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IdezJobsWeb.Areas.Administrative.Controllers
{
	[HandleError(View = "Error")]
	[Authorize(Roles = "Administrador")]
	public class HomeController : Controller
	{
		//
		// GET: /Administrative/Home/

		public ActionResult Index( )
		{
			return View( );
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
