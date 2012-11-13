using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;

namespace IdezJobsWeb.Areas.Business.Controllers
{
	[HandleError(View = "Error")]
	[Authorize(Roles = "Company")]
	public class VacancyController : Controller
	{
		private IContextData _ContextDataVacancy = new ContextDataNH( );


		public ActionResult Index( )
		{
		  
			return View( );
		}

		public ActionResult ListVacancy( )
		{
			IList<Vacancy> listVancancy = null;
			listVancancy = _ContextDataVacancy.GetAll<Vacancy>( )
						  .ToList( );
			return View(listVancancy);
		}

		public ActionResult ListVacancyDetails( )
		{
			IList<Vacancy> list = null;
			list = _ContextDataVacancy.GetAll<Vacancy>( )
			 .OrderByDescending(x => x.RegistrionDate).ToList( );
			return View(list);
		}

		public ActionResult Inscritos(int id)
		{
			IList<JobCandidate> listaCandidatos = null;
			listaCandidatos = _ContextDataVacancy.GetAll<JobCandidate>( )
									 .Where(x => x.JobCandidato.Id == id).ToList( );
			IList<Profile> listaPerfil = null;
			foreach (var item in listaCandidatos)
			{
				listaPerfil = _ContextDataVacancy.GetAll<Profile>( )
							  .Where(x => x.Id == item.UserJobs.Id.ToString( )).ToList( );

			}
			if (listaPerfil == null)
			{
				return RedirectToAction("ErroVaga");
			}

			return View(listaPerfil);

		}

		public ActionResult ErroVaga( )
		{
			return View( );
		}

		public ActionResult Details(int id)
		{
			Vacancy DetailsVacancy = _ContextDataVacancy.Get<Vacancy>(id);
			return View(DetailsVacancy);
		}


		public ActionResult Create( )
		{
			ViewBag.PerfilVaga = new SelectList(_ContextDataVacancy.GetAll<ProfileVacancy>( ), "Id", "Myprofile").ToList( );

			return View( );
		}


		[HttpPost]
		public ActionResult Create(Vacancy vacancy)
		{
			vacancy.RegistrionDate = DateTime.Now;
			ModelState["ProfileVacancy.Myprofile"].Errors.Clear( );
			ModelState["Status"].Errors.Clear( );


			string CompanyName = User.Identity.Name;

			if (ModelState.IsValid)
			{

				if (vacancy.RegistrationDeadline > DateTime.Now.Date)
				{
					ModelState.AddModelError("", "A data deve ser maior que a data atual.");
				}
				vacancy.Benefits = vacancy.Benefits.ToLower( );
				vacancy.Description = vacancy.Description.ToLower( );
				vacancy.OfficeHours = vacancy.OfficeHours.ToLower( );
				vacancy.ProfileVacancy = _ContextDataVacancy.Get<ProfileVacancy>(vacancy.ProfileVacancy.Id);
				vacancy.Status = _ContextDataVacancy.GetAll<Status>( ).Where(x => x.Description == "Aberto").First( );
				vacancy.CompanyName = _ContextDataVacancy.GetAll<Company>( )
									  .Where(x => x.Name == CompanyName).First( );
				_ContextDataVacancy.Add<Vacancy>(vacancy);

				_ContextDataVacancy.SaveChanges( );

				return RedirectToAction("Sucess", "Home");


			}
			ViewBag.PerfilVaga = new SelectList(_ContextDataVacancy.GetAll<ProfileVacancy>( ), "Id", "Myprofile", vacancy.ProfileVacancy);
			ViewBag.StatusVaga = new SelectList(_ContextDataVacancy.GetAll<Status>( ), "Code", "Description", vacancy.Status);

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
			VacancyEdit.OfficeHours = vacancy.OfficeHours.ToLower( );
			VacancyEdit.Description = vacancy.Description.ToLower( );
			VacancyEdit.Benefits = vacancy.Benefits.ToLower( );
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

		public ActionResult ShowProfile(int id)
		{
			var listaNumerosPerfis = (from c in _ContextDataVacancy.GetAll<JobCandidate>( )
									.Where(x => x.JobCandidato.Id == id)
									  select c.UserJobs.Id).ToList( );

			IList<Profile> listaPerfis = null;
			foreach (var item in listaNumerosPerfis)
			{
				listaPerfis = _ContextDataVacancy.GetAll<Profile>( )
							  .Where(x => x.IdUser == item.ToString( ))
							  .ToList( );

			}
			return View(listaPerfis);
		}


		public ActionResult ShowUser(int id)
		{

			string Description = (from c in _ContextDataVacancy.GetAll<Vacancy>( )
								  .Where(x => x.Id == id)
								  select c.Description.ToLower()).First( );
			string[] Letras = Description.Split(new char[] { ' ' });

			IList<Profile> profileUser = null;
			IList<Profile> list = new List<Profile>( );
			int achei = 0;
			foreach (var item in Letras)
			{
				profileUser = _ContextDataVacancy.GetAll<Profile>( )
							  .Where(x => x.Interests.ToLower().Contains(item.ToLower().ToString( ))).ToList( );

				if (profileUser.Count( ) > 0)
				{
					achei++;
					list = profileUser;
				}

			}

			return View(list);
		}


	}
}
