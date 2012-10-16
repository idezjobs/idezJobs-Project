﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using System.Web.Helpers;
using IdezJobsWeb.Models.Context;


namespace IdezJobsWeb.Areas.Business.Controllers
{

	public class FazerPublicacaoEmailController : Controller
	{
		private IContextData _ContextoEmail = new ContextDataNH( );
		public ActionResult Index( )
		{
			return View( );
		}

		public ActionResult SendMailForJobs( )
		{
			return View( );
		}

		[HttpPost]
		public ActionResult SendMailForJobs(Email email)
		{

			try
			{												
				WebMail.Send(email.EmailUser, email.Subject, email.Body);

			}
			catch (Exception e)
			{

				ModelState.AddModelError("", "Erro:" + e.ToString( ));
			} 

			_ContextoEmail.Add<Email>(email);
			_ContextoEmail.SaveChanges( ); 

			return RedirectToAction("SucessEmail");

		}

		public ActionResult SucessEmail( )
		{
			return View( );
		}




		public ActionResult EnviarEmail( )
		{
			SendEmailController enviar = new SendEmailController( );
			foreach (var item in listaEmail( ).ToList( ))
			{
				ViewBag.nome1 = item.Name;
				ViewBag.email1 = item.Email;
				enviar.EmailParaFaculdade(listaEmail( ).ToList( )).Deliver( );
			}
			return Content("Consegui enviar seu email");
		}




		public IList<User> listaEmail( )
		{
			List<User> lista = new List<User>( );
			User usuario = new User( );
			usuario.Email = "trwww@htomail.com";
			usuario.DateRegister = new DateTime(1987, 07, 10);
			usuario.Name = "ademi";
			usuario.Type = "Admin";

			lista.Add(usuario);

			User usuario1 = new User( );

			usuario1.Email = "ademivieir@htomail.com";
			usuario1.DateRegister = new DateTime(1988, 08, 10);
			usuario1.Name = "joao";
			usuario1.Type = "Teste";

			lista.Add(usuario1);

			User usuario2 = new User( );

			usuario2.Email = "okii@htomail.com";
			usuario2.DateRegister = new DateTime(1988, 08, 10);
			usuario2.Name = "paty";
			usuario2.Type = "Teste";

			lista.Add(usuario2);


			User usuario3 = new User( );

			usuario3.Email = "teet@htomail.com";
			usuario3.DateRegister = new DateTime(1988, 08, 10);
			usuario3.Name = "Ok Ok";
			usuario3.Type = "Teste";

			lista.Add(usuario3);



			return lista.ToList( );

		}

	}
}