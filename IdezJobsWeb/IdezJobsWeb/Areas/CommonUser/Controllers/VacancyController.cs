using System;
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

			return RedirectToAction("Sucess", "Home");
			
		}

		



	}
}
