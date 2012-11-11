using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;

namespace IdezJobsWeb.Controllers
{
	public class HomeController : Controller
	{


		public ActionResult Index( )
		{
			Update( );
			return View( );
		} 


		public void Update( )
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
	}
}
