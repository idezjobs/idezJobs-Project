using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net.Mvc;

namespace IdezJobsWeb.Areas.Business.Controllers
{
	[Authorize(Roles = "Administrador")]
	public class FazerPublicacaoEmailController : Controller
	{
		//
		// GET: /Business/FazerPublicacaoEmail/

		public ActionResult Index( )
		{
			return View( );
		}

		public ActionResult EnviarEmailParaFaculdade( )
		{
			new SendEmailController( ).EmailParaFaculdade( ).Deliver( );
			return Content("Email da Faculdade Enviado Com Sucesso");
		}

	}
}
