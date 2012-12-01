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

	[HandleError(View = "Error")]
	[Authorize(Roles = "Administrador")]
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
			ViewBag.PerfilVaga = new SelectList(_ContextDataVacancy.GetAll<ProfileVacancy>( ), "Id", "Myprofile").ToList( );
			ViewBag.Company = new SelectList(_ContextDataVacancy.GetAll<Company>( ), "Id", "Name").ToList( );
			ViewBag.StatusVaga = new SelectList(_ContextDataVacancy.GetAll<Status>( ), "Code", "Description").ToList( );
			return View( );
		}


		[HttpPost]
		public ActionResult Create(Vacancy vacancy)
		{
			vacancy.RegistrionDate = DateTime.Now;
			ModelState["ProfileVacancy.Myprofile"].Errors.Clear( );
			ModelState["CompanyName.Name"].Errors.Clear( );
			ModelState["CompanyName.Email"].Errors.Clear( );
			ModelState["Status"].Errors.Clear( );


			if (ModelState.IsValid)
			{

				if (vacancy.RegistrationDeadline.Date <= DateTime.Now.Date)
				{
					ModelState.AddModelError("", "A data deve ser maior que a data atual.");
				}
				vacancy.Benefits = vacancy.Benefits.ToLower();
				vacancy.Description = vacancy.Description.ToLower();
				vacancy.OfficeHours = vacancy.OfficeHours.ToLower();
				vacancy.ProfileVacancy = _ContextDataVacancy.Get<ProfileVacancy>(vacancy.ProfileVacancy.Id);
				vacancy.CompanyName = _ContextDataVacancy.Get<Company>(vacancy.CompanyName.Id);
				vacancy.Status = _ContextDataVacancy.GetAll<Status>( ).Where(x => x.Description == "Aberto").First( );
				vacancy.KeyWords = RemoveString.GenerateKeyWords(vacancy.Description);

				if (vacancy.Status == null)
				{
					ModelState.AddModelError("", "O Status não pode ser vazio.");
				}
				_ContextDataVacancy.Add<Vacancy>(vacancy);

				_ContextDataVacancy.SaveChanges( );

				return RedirectToAction("Sucess", "Home");


			}

			ViewBag.PerfilVaga = new SelectList(_ContextDataVacancy.GetAll<ProfileVacancy>( ), "Id", "Myprofile", vacancy.ProfileVacancy.Id);
			ViewBag.StatusVaga = new SelectList(_ContextDataVacancy.GetAll<Status>( ), "Code", "Description", vacancy.Status.Code);
			ViewBag.Company = new SelectList(_ContextDataVacancy.GetAll<Company>( ), "Id", "Name", vacancy.CompanyName.Id);


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
			VacancyEdit.OfficeHours = vacancy.OfficeHours.ToLower();
			VacancyEdit.Benefits = vacancy.Benefits.ToLower();
			VacancyEdit.Description = vacancy.Description.ToLower();
			VacancyEdit.KeyWords = RemoveString.GenerateKeyWords(vacancy.Description);
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

			_ContextDataVacancy.Delete<Vacancy>(VacancyDelete);
			_ContextDataVacancy.SaveChanges( );
			return RedirectToAction("Sucess", "Home");
		}
	}
}
