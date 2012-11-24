using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;
using Telerik.Web.Mvc;
using System.Web.Helpers;

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
			string nomeUsuarioLogado = User.Identity.Name;
			IList<Vacancy> list = null;
			list = _ContextDataVacancy.GetAll<Vacancy>( )
				  .Where(x => x.CompanyName.Name == nomeUsuarioLogado)
				  .OrderByDescending(x => x.RegistrionDate).ToList( );
			return View(list);
		}

		public ActionResult Inscritos(int id)
		{
			//IList<JobCandidate> listaCandidatos = null;
			//listaCandidatos = _ContextDataVacancy.GetAll<JobCandidate>( )
			//                         .Where(x => x.JobCandidato.Id == id).ToList( );
			//IList<Profile> listaPerfil = null;
			//foreach (var item in listaCandidatos)
			//{
			//    listaPerfil = _ContextDataVacancy.GetAll<Profile>( )
			//                  .Where(x => x.Id == item.UserJobs.Id.ToString( )).ToList( );

			//}
			//if (listaPerfil == null)
			//{
			//    return RedirectToAction("ErroVaga");
			//}

			return View( );

		}

		[GridAction]
		public ActionResult Inscritos3(int id)
		{

			var listProfileInscritos = (from jb in _ContextDataVacancy.GetAll<JobCandidate>( )
										.Where(x => x.JobCandidato.Id == id)
										join pf in _ContextDataVacancy.GetAll<Profile>( )
										on jb.UserJobs.Id equals pf.IdUser
										select new { FirstName = pf.FirstName, LastName = pf.LastName, PublicUrl = pf.PublicUrl, Headline = pf.Headline, Industry = pf.Industry });

			return View(new GridModel(listProfileInscritos.ToList( )));
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
				vacancy.Benefits = vacancy.Benefits;
				vacancy.Description = vacancy.Description;
				vacancy.OfficeHours = vacancy.OfficeHours;
				vacancy.ProfileVacancy = _ContextDataVacancy.Get<ProfileVacancy>(vacancy.ProfileVacancy.Id);
				vacancy.Status = _ContextDataVacancy.GetAll<Status>( ).Where(x => x.Description == "Aberto").First( );
				vacancy.CompanyName = _ContextDataVacancy.GetAll<Company>( )
									  .Where(x => x.Name == CompanyName).First( );
				string Body = " Encontra-se disponível uma vaga na empresa:" + CompanyName + "\n<br/>" +
							  "Com a seguinte descrição : " + vacancy.Description + "\n<br/>" +
							  "Benefícios: " + vacancy.Benefits + "\n<br/>" +
							  "Números de Vagas: " + vacancy.JobsNumber + "\n<br/>" +
							  "Data de inscrição: " + " de: " + DateTime.Now.Date + " Até: " + vacancy.RegistrationDeadline.Date + "\n<br/>" +
							   "Para Visualizar a Vaga Clique <a href='http://localhost:1677/CommonUser/Vacancy/ListVacancyOpen'>Aqui</a>";



				_ContextDataVacancy.Add<Vacancy>(vacancy);

				_ContextDataVacancy.SaveChanges( );

				IList<Profile> ListMail = _ContextDataVacancy.GetAll<Profile>( ).ToList( );
				foreach (var item in ListMail)
				{
					if (item.EmailAddress != null)
					{
						WebMail.Send(item.EmailAddress, "Vaga disponível na empresa : " + CompanyName, item.FirstName + Body);

					}
				}

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
			return RedirectToAction("ListVacancyDetails", "Vacancy");
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
			IList<JobCandidate> CandidateList = _ContextDataVacancy.GetAll<JobCandidate>( )
												.Where(x => x.JobCandidato.Id == vacancy.Id).ToList( );
			if (CandidateList.Count( ) >= 1)
			{
				return RedirectToAction("UsersJobs", "Vacancy");
			}

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
							  .Where(x => x.IdUser == item)
							  .ToList( );
			}

			if (listaNumerosPerfis.Count( ) >= 1)
			{

				return View(listaPerfis);
			}
			else
			{
				return RedirectToAction("NoRecords", "Vacancy");
			}

		}

		public ActionResult NoRecords( )
		{
			return View( );
		}

		public ActionResult UsersJobs( )
		{
			return View( );
		}


		public ActionResult ShowUser(int id)
		{

			string Description = (from c in _ContextDataVacancy.GetAll<Vacancy>( )
								  .Where(x => x.Id == id)
								  select c.Description.ToLower( )).First( );
			string[] Letras = Description.Split(new char[] { ' ' });

			IList<Profile> profileUser = _ContextDataVacancy.GetAll<Profile>( ).ToList( );
			IList<Profile> listCopy = new List<Profile>( );
			int achei = 0;

			foreach (var item in profileUser)
			{
				string IntersectIndividual = (from c in _ContextDataVacancy.GetAll<Profile>( )
								   .Where(x => x.Code ==item.Code)
											  select c.Interests.ToLower( )).First( );
				string[] PalavrasInteresseIndividual = IntersectIndividual.Split(new char[] { ' ' });
				foreach (var palavrasDaDescricao in Letras)
				{
					foreach (var itemInteresse in PalavrasInteresseIndividual)
					{
					 var list = (from c in profileUser
						         where palavrasDaDescricao.ToLower().Contains(itemInteresse.ToLower())
								 select c).ToList();

					if (list.Count( ) > 0)
					{
						achei++;
						listCopy = list;
					}	
					}
				}
			} 
			return View(listCopy);
		}


	}
}
