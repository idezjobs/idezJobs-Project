﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;
using System.Web.Security;
using IdezJobsWeb.Controllers;

namespace IdezJobsWeb
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute( ));
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				new string[] { "IdezJobsWeb.Controllers" }// Parameter defaults
			);

		}

		protected void Application_Start( )
		{
			AreaRegistration.RegisterAllAreas( );

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

		//	HomeController 	UpdateVacancy = new HomeController();
			//UpdateVacancy.Update( );
			

			if (System.Web.Security.Roles.GetAllRoles( ).Length == 0)
			{
				using (IContextData ctx = new ContextDataNH( ))
				{
					ctx.setup( );
					System.Web.Security.Roles.CreateRole("Administrador");
					System.Web.Security.Roles.CreateRole("Company");
					System.Web.Security.Roles.CreateRole("Student");

					MembershipCreateStatus status;
					Membership.CreateUser("IdezJobs", "JobsIdezypa", "idezjobs@gmail.com", null, null, true, out status);
					Membership.CreateUser("Estudante", "senha123", "estudante@gmail.com", null, null, true, out status);
					Membership.CreateUser("Empresa", "empresa123", "empresa@gmail.com", null, null, true, out status);
					if (status == MembershipCreateStatus.Success)
					{
						Administrator Administ = new Administrator( );
						Administ.Name = "IdezJobs";
						Administ.Password = "JobsIdezypa";
						Administ.Email = "idezjobs@gmail.com";
						Administ.Type = "Administrator";

						ctx.Add<Administrator>(Administ);
						ctx.SaveChanges( );


						User userEstudante = new User( );
						userEstudante.DateRegister = DateTime.Now;
						userEstudante.Email = "estudante@gmail.com";
						userEstudante.Name = "Estudante";
						userEstudante.Token = "Token";
						userEstudante.Type = "Estudante";

						ctx.Add<User>(userEstudante);
						ctx.SaveChanges( );

						User userEmpresa = new User( );
						userEmpresa.DateRegister = DateTime.Now;
						userEmpresa.Email = "empresa@gmail.com";
						userEmpresa.Name = "Empresa";
						userEmpresa.Token = "Token Empresa";
						userEmpresa.Type = "Company";

						ctx.Add<User>(userEmpresa);
						ctx.SaveChanges( );


						Status s1 = new Status( );
						s1.Description = "Aberto";
						ctx.Add<Status>(s1);
						ctx.SaveChanges( );


						Status s2 = new Status( );
						s2.Description = "Fechado";
						ctx.Add<Status>(s2);
						ctx.SaveChanges( );


						Roles.AddUserToRole("IdezJobs", "Administrador");
						Roles.AddUserToRole("Estudante", "Student");
						Roles.AddUserToRole("Empresa", "Company");

						ctx.Dispose( );
					}
				}
			}
		}
	}
}