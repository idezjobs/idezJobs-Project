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

		public ActionResult listaPerfil( )
		{
			IList<Profile> list = null;
			using (IContextData c = new ContextDataNH( ))
			{
				list = c.GetAll<Profile>( ).ToList( );
			}

			return View(list);
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

		public ActionResult Hire( )
		{
			using (IContextData _ContextHire = new ContextDataNH( ))
			{
				IList<Vacancy> listaVagas = null;
				listaVagas = _ContextHire.GetAll<Vacancy>( )
							 .Where(x => x.RegistrationDeadline.Day == DateTime.Now.Day - 1 && x.RegistrationDeadline.Month == DateTime.Now.Month || x.RegistrationDeadline.Month == DateTime.Now.Month - 1 && x.RegistrationDeadline.Year == DateTime.Now.Year && x.Status.Code == 2)
							 .ToList( );
				if (listaVagas.Count( ) > 0)
				{
					IList<JobCandidate> listJobs = null;
					IList<Profile> listProfile = null;
					string EmailsProfile = null;
					IList<Profile> listProfileCopy = new List<Profile>( );
					foreach (var itemListaVagas in listaVagas)
					{
						listJobs = _ContextHire.GetAll<JobCandidate>( )
								   .Where(x => x.JobCandidato.Id == itemListaVagas.Id).ToList( );
						if (listJobs.Count( ) > 0)
						{

							string Description = itemListaVagas.KeyWords.ToLower( );

							string[] Letras = Description.Split(new char[] { ',' });


							foreach (var itemJobs in listJobs)
							{
								listProfile = _ContextHire.GetAll<Profile>( )
											  .Where(x => x.IdUser == itemJobs.UserJobs.Id)
											  .ToList( );
								//talvez precise verificar se não tem nenhum usuário inscrito

								foreach (var itemProfile in listProfile)
								{
									string IntersectIndividual = (from c in _ContextHire.GetAll<Profile>( )
																  .Where(x => x.Code == itemProfile.Code)
																  select c.KeyWords.ToLower( )).First( );

									if (IntersectIndividual != null)
									{
										string[] PalavrasInteresseIndividual = IntersectIndividual.Split(new char[] { ',' });
										int encontrado = 0;
										foreach (var palavrasDaDescricao in Letras)
										{

											foreach (var itemInteresse in PalavrasInteresseIndividual)
											{

												var list = (from c in listProfile
															where palavrasDaDescricao.ToLower( ).Contains(itemInteresse.ToLower( ))
															select c).ToList( );

												if (list.Count( ) > 0)
												{
													encontrado++;
													var listScoreUser = _ContextHire.GetAll<Score>( )
																		.Where(x => x.ScoreProfileUser.Code == itemProfile.Code && x.ScoreVacancy.Id == itemListaVagas.Id)
																		.ToList( );
													if (listScoreUser.Count( ) >= 1)
													{
														Score ScoreUpdate = _ContextHire.GetAll<Score>( )
																		  .Where(x => x.ScoreProfileUser.Code == itemProfile.Code && x.ScoreVacancy.Id == itemListaVagas.Id)
																		.First( );
														ScoreUpdate.ScoreProfileUser = _ContextHire.Get<Profile>(itemProfile.Code);
														ScoreUpdate.ScoreVacancy = _ContextHire.Get<Vacancy>(itemListaVagas.Id);
														ScoreUpdate.ScoreUser = encontrado;
														_ContextHire.SaveChanges( );


													}
													else
													{
														Score ScoreUser = new Score( );
														ScoreUser.ScoreProfileUser = _ContextHire.Get<Profile>(itemProfile.Code);
														ScoreUser.ScoreVacancy = _ContextHire.Get<Vacancy>(itemListaVagas.Id);
														ScoreUser.ScoreUser = encontrado;
														_ContextHire.Add<Score>(ScoreUser);
														_ContextHire.SaveChanges( );

													}

													Profile ProfileSendMail = _ContextHire.Get<Profile>(itemProfile.Code);
													listProfileCopy.Add(ProfileSendMail);

												}


											}
										}
									}
									  //aqui estudante antes
									
									//estava aqui

								}

								//agora envio de estudante
								foreach (var itemEmail in listProfileCopy.Distinct( ))
								{
									WebMail.Send(itemEmail.EmailAddress, "Parabéns você esta concorrendo a vaga de " + itemListaVagas.ProfileVacancy.Myprofile, "Aguarde o contato da empresa " + itemListaVagas.CompanyName.Name);

								}

							}					

							
						}

						var listScoreVacancy = (from sc in _ContextHire.GetAll<Score>( )
													  .Where(x => x.ScoreVacancy.Id == itemListaVagas.Id )
												join pf in _ContextHire.GetAll<Profile>( )
												on sc.ScoreProfileUser.Code equals pf.Code
												orderby sc.ScoreUser descending
												select new { Nome = pf.FirstName, URLPública = pf.PublicUrl, Email = pf.EmailAddress + "</br>" });
						if (listScoreVacancy.Count() > 0)
						{

						EmailsProfile = String.Join(",", listScoreVacancy);
						
						string BodyMessageCompany = itemListaVagas.CompanyName.Name + "," + " \n" +
							   "Segue abaixo a relação dos candidatos que de acordo com seu perfil estão aptos a vaga descrita:" + "(" + itemListaVagas.ProfileVacancy.Myprofile + ")" + "\n" +
							  "classificada em ordem decrescente em relação aos que melhores se enquadram na vaga, caso queira analisar o perfil público" + "\n" +
							  "do candidato clique no link correspondente. " + EmailsProfile;
						WebMail.Send(itemListaVagas.CompanyName.Email, "Lista de Classificados pelo IdezJobs ", BodyMessageCompany);
						}
					}

				}


			}
			return View( );
		}

	}
}
