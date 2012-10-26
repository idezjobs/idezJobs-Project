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
		
		var ListVacancy = (from c in _ContextoVaga.GetAll<Vacancy>().Where(x => x.Status.Description == "Aberto")
		               select new {DataLimite = c.RegistrationDeadline, DescricaoVaga = c.Description});
		           
		             
		return View(ListVacancy);
		}



	}
}
