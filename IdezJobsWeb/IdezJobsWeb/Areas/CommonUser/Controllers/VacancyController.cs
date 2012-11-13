﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;

namespace IdezJobsWeb.Areas.CommonUser.Controllers
{
	[Authorize(Roles = "Student")]
	[HandleError(View = "Error")]
	public class VacancyController : Controller
	{
		private IContextData _ContextoVaga = new ContextDataNH( );

		public ActionResult Index( )
		{
			return View( );
		}

		public ActionResult ListVacancyOpen( )
		{

			var ListVacancy = _ContextoVaga.GetAll<Vacancy>( ).Where(x => x.Status.Description == "Aberto" && x.RegistrationDeadline >= DateTime.Now.Date).ToList( );

			return View(ListVacancy);
		}

		public ActionResult Register(int id)
		{
			Vacancy RegisterVacancy = _ContextoVaga.Get<Vacancy>(id);
			string UserName = User.Identity.Name;
			long UserIdBase = (from c in _ContextoVaga.GetAll<User>( )
							 .Where(x => x.Name == UserName)
							   select c.Id).First( );
			JobCandidate Jobs = new JobCandidate( );
			Jobs.AdditionalInformation = "Data da Inscrição: " + DateTime.Now;
			Jobs.JobCandidato = _ContextoVaga.Get<Vacancy>(RegisterVacancy.Id);
			Jobs.UserJobs = _ContextoVaga.Get<User>(UserIdBase);
			var verificaVaga = _ContextoVaga.GetAll<JobCandidate>( )
							   .Where(x => x.JobCandidato.Id == id && x.UserJobs.Id == UserIdBase).ToList( );
			if (verificaVaga.Count( ) >= 1)
			{
				return RedirectToAction("Duplicity");
			}
			else
			{
				_ContextoVaga.Add<JobCandidate>(Jobs);
				_ContextoVaga.SaveChanges( );

			}


			return RedirectToAction("Sucess", "Home");

		}

		public ActionResult Duplicity( )
		{
			return View( );
		}

		public ActionResult SearchVacancyOpen( )
		{
			return View( );
		}

		[HttpPost]
		public ActionResult SearchVacancyOpen(string description)
		{
			string descriptonUpper = description.ToUpper( );
			return View("ResultSearch", _ContextoVaga.GetAll<Vacancy>( ).Where(x => x.Description.Contains(descriptonUpper)));
		}

		public ActionResult ResultSearch( )
		{
			return View( );
		}

		public ActionResult ShowVacancyProfile( )
		{
			string MyName = User.Identity.Name;
			string MyToken = (from c in _ContextoVaga.GetAll<User>( )
							.Where(x => x.Name == MyName)
							  select c.Token).First( );

			string MyProfile = (from c in _ContextoVaga.GetAll<Profile>( )
							.Where(x => x.Id == MyToken)
								select c.Interests).First( );

			string ListIntersectes = MyProfile;
			string[] Letras = ListIntersectes.Split(new char[] { ' ' });
			var Query = from w in Letras where w == "," select w;

			IList<Vacancy> listVacancy = null;
			IList<Vacancy> CopyVacancy = _ContextoVaga.GetAll<Vacancy>( ).ToList( );
			int achei = 0;
			foreach (var item in Letras)
			{
				listVacancy = _ContextoVaga.GetAll<Vacancy>( )
					  .Where(x => x.Description.Contains(item.ToString( ))).ToList( );

				if (listVacancy.Count( ) > 0)
				{
					achei++;
					CopyVacancy = listVacancy;
				}

			}

			return View(CopyVacancy);
		}

	}
}
