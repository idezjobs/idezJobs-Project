using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;
using System.Web.Helpers;

namespace IdezJobsWeb.Controllers
{
	public class HomeController : Controller
	{


		public ActionResult Index( )
		{
			Update( );
			Hire( );

			return View( );
		}



		private void Update( )
		{
			using (IContextData Update = new ContextDataNH( ))
			{
				IList<Vacancy> VacancyList = null;
				VacancyList = Update.GetAll<Vacancy>( )
							.Where(x => x.RegistrationDeadline < DateTime.Now.Date && x.Status.Description == "Aberto").ToList( );

				foreach (var item in VacancyList)
				{
					Vacancy Va = Update.Get<Vacancy>(item.Id);
					Va.Status = Update.GetAll<Status>( ).Where(x => x.Description == "Fechado").First( );
					Update.SaveChanges( );


				}

				Update.Dispose( );

			}
		}

		private void Hire( )
		{
			using (IContextData Hir = new ContextDataNH( ))
			{
				IList<Vacancy> listaVagas = null;
				listaVagas = Hir.GetAll<Vacancy>( )
							 .Where(x => x.RegistrationDeadline.Day == DateTime.Now.Day - 1 && x.Status.Code == 2)
							 .ToList( );
				if (listaVagas.Count( ) > 0)
				{
					IList<JobCandidate> listJobs = null;
					IList<Profile> listProfile = null;
					IList<Profile> listProfileCopy = new List<Profile>( );
					foreach (var item in listaVagas)
					{
						listJobs = Hir.GetAll<JobCandidate>( )
								   .Where(x => x.JobCandidato.Id == item.Id).ToList( );

						foreach (var itemJobs in listJobs)
						{
							listProfile = Hir.GetAll<Profile>( )
										  .Where(x => x.IdUser == itemJobs.UserJobs.Id)
										  .ToList( );


							string Description = item.Description.ToLower( );

							string[] Letras = Description.Split(new char[] { ' ' });

							foreach (var itemLe in Letras)
							{
								listProfileCopy = (from c in listProfile
												 .Where(x => x.Interests.ToLower( ).Contains(itemLe.ToLower( ).ToString( )))
												   select c).ToList( );

								if (listProfileCopy.Count( ) > 0)
								{
									 
									foreach (var itemEmail in listProfileCopy)
									{
									 WebMail.Send(itemEmail.EmailAddress,"Parabéns você esta concorrendo a vaga de " + item.ProfileVacancy.Myprofile,"Aguarde o contato da empresa " + item.CompanyName.Name);
									 
									 										
									}

								}
							

							}



						}

					}

				}

			}


		}

	}
}
