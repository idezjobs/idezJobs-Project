using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;
using Telerik.Web.Mvc;

namespace IdezJobsWeb.Areas.Administrative.Controllers
{
	//Vaga

	[Authorize(Roles="Administrador")]
	public class VacancyController : Controller
	{
		private IContextData _ContextDataVacancy = new ContextDataNH( );


		public ActionResult Index( )
		{
			return View( );
		}

		public ActionResult ListVacancy( )
		{

			return View( );
		}

		public ActionResult ListVacancyDetails( )
		{
			IList<Vacancy> listVacancy = null;
			listVacancy = _ContextDataVacancy.GetAll<Vacancy>( ).ToList( );
			return View(listVacancy);
		}

		[GridAction]
		public ActionResult _ListVacancy( )
		{
			IList<Vacancy> listVancancy = null;
			listVancancy = _ContextDataVacancy.GetAll<Vacancy>( ).ToList( );
			return View(new GridModel(listVancancy));
		}



		public ActionResult Details(int id)
		{
			Vacancy DetailsVacancy = _ContextDataVacancy.Get<Vacancy>(id);
			return View(DetailsVacancy);
		}


		public ActionResult Create( )
		{
			return View( );
		}


		[HttpPost]
		public ActionResult Create(Vacancy vacancy)
		{
			if (ModelState.IsValid)
			{
				_ContextDataVacancy.Add<Vacancy>(vacancy);
				_ContextDataVacancy.SaveChanges( );

				return RedirectToAction("Sucess", "Home");
			}

			return View( );
		}



		public ActionResult Edit(int id)
		{
			Vacancy VacancyEdit = _ContextDataVacancy.Get<Vacancy>(id);
			return View(VacancyEdit);
		}


		[HttpPost]
		public ActionResult Edit(Vacancy vacancy)
		{

			Vacancy VacancyEdit = _ContextDataVacancy.Get<Vacancy>(vacancy.Id);
			TryUpdateModel(VacancyEdit);
			_ContextDataVacancy.SaveChanges( );
			return RedirectToAction("Sucess", "Home");
		}


		public ActionResult Delete(int id)
		{
			Vacancy VacancyDelete = _ContextDataVacancy.Get<Vacancy>(id);

			return View(VacancyDelete);
		}



		[HttpPost]
		public ActionResult Delete(Vacancy vacancy)
		{
			Vacancy VacancyDelete = _ContextDataVacancy.Get<Vacancy>(vacancy.Id);

			_ContextDataVacancy.Delete<Vacancy>(vacancy);
			_ContextDataVacancy.SaveChanges( );
			return RedirectToAction("Sucess", "Home");
		}
	}
}
