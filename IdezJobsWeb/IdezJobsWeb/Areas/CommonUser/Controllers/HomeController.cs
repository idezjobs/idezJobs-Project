using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}
