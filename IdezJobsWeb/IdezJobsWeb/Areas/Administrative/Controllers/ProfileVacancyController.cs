using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models.Context;
using IdezJobsWeb.Models;

namespace IdezJobsWeb.Areas.Administrative.Controllers
{
	[HandleError(View = "Error")]
	[Authorize(Roles = "Administrador")]
	public class ProfileVacancyController : Controller
	{
		private IContextData _ContextoProfile = new ContextDataNH( );

		public ActionResult Index( )
		{
			return View( );
		}

		public ActionResult Create( )
		{
			return View( );
		}

		[HttpPost]
		public ActionResult Create(ProfileVacancy perfil)
		{
			if (ModelState.IsValid)
			{

				_ContextoProfile.Add<ProfileVacancy>(perfil);
				_ContextoProfile.SaveChanges( );
			}
			return View("ListProfileVacancy", _ContextoProfile.GetAll<ProfileVacancy>( ).ToList( ));
		}

		public ActionResult Details(int id)
		{
			ProfileVacancy perfil = _ContextoProfile.Get<ProfileVacancy>(id);
			return View(perfil);

		}

		public ActionResult Edit(int id)
		{
			ProfileVacancy perfil = _ContextoProfile.Get<ProfileVacancy>(id);
			return View(perfil);
		}

		[HttpPost]
		public ActionResult Edit(ProfileVacancy perfil)
		{

			ProfileVacancy perfilAlterado = _ContextoProfile.Get<ProfileVacancy>(perfil.Id);

			if (ModelState.IsValid)
			{
				TryUpdateModel(perfilAlterado);
				_ContextoProfile.SaveChanges( );
				return View("ListProfileVacancy", _ContextoProfile.GetAll<ProfileVacancy>( ).ToList( ));
			}
			return View( );
		}


		public ActionResult Delete(int id)
		{
			ProfileVacancy perfil = _ContextoProfile.Get<ProfileVacancy>(id);
			return View(perfil);
		}

		[HttpPost]
		public ActionResult Delete(ProfileVacancy perfil)
		{

			ProfileVacancy perfilExcluido = _ContextoProfile.Get<ProfileVacancy>(perfil.Id);

			if (ModelState.IsValid)
			{
				_ContextoProfile.Delete<ProfileVacancy>(perfilExcluido);
				_ContextoProfile.SaveChanges( );
				return View("Sucess");
			}
			return View("Sucess");
		}

		public ActionResult Sucess( )
		{
			return View( );
		}




	}
}
