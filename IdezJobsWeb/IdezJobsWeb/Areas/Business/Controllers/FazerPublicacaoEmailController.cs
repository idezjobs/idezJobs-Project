using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;


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

		public ActionResult EnviarEmail( )
		{
		 var listaemail = lista();
			foreach (var item in lista())
			{
				new SendEmailController( ).EmailParaFaculdade(lista().ToList()).Deliver( );
			}
		 

			return Content("Consegui enviar seu email");
		}

		public IList<User> lista()
		{
		User usuario = new User();
		usuario.Email = "ademivieira@htomail.com";
		usuario.DateRegister = new DateTime(1987,07,10);
		usuario.Name = "ademi";
		usuario.Type = "Admin";

		User usuario1 = new User( );

		usuario1.Email = "ademivieir@htomail.com";
		usuario1.DateRegister = new DateTime(1988, 08, 10);
		usuario1.Name = "ademivr";
		usuario1.Type = "Teste";


		User usuario2 = new User( );

		usuario2.Email = "ademivieir@htomail.com";
		usuario2.DateRegister = new DateTime(1988, 08, 10);
		usuario2.Name = "ademivr";
		usuario2.Type = "Teste";


		List<User> lsita = new List<User>();
		 lsita.Add(usuario);
		 lsita.Add(usuario1);
		 lsita.Add(usuario2);
		return lsita;

		}

	}
}
