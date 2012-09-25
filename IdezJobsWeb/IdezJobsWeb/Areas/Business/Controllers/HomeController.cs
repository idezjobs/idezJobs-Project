using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdezJobsWeb.Areas.Business.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Business/Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sucess( )
        {
            return View( );
        }

    }
}
