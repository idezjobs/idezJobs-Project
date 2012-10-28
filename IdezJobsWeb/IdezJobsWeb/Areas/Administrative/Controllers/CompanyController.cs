using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;
using Telerik.Web.Mvc;
using System.Web.Security;

namespace IdezJobsWeb.Areas.Administrative.Controllers
{
	[HandleError(View = "Error")]
	[Authorize(Roles = "Administrador")]
	public class CompanyController : Controller
	{
		private IContextData _ContextData = new ContextDataNH( );

		public ActionResult Index( )
		{
			return View( );
		}


		public ActionResult ListDetails( )
		{
			IList<Company> listCompany = null;
			listCompany = _ContextData.GetAll<Company>( ).ToList( );

			return View(listCompany);
		}

		public ActionResult ListCompany( )
		{
			return View( );
		}

		[GridAction]
		public ActionResult _ListCompany( )
		{
			IList<Company> listCompany = null;

			listCompany = _ContextData.GetAll<Company>( ).ToList( );
			return View(new GridModel(listCompany));
		}


		//
		// GET: /Administrative/Company/Details/5

		public ActionResult Details(int id)
		{
			Company CompanyDetails = _ContextData.Get<Company>(id);
			ViewBag.NumeroVagas = (from c in _ContextData.GetAll<Vacancy>( )
								   .Where(x => x.CompanyName.Contains(CompanyDetails.Name))
								   select c.JobsNumber).First( );
			return View(CompanyDetails);
		}

		public ActionResult JobsAvailable(string id)
		{
			IList<Vacancy> ListVancancyAvailable = _ContextData.GetAll<Vacancy>( )
												  .Where(x => x.CompanyName == id)
												  .ToList( );
			ViewBag.Empresa = id;
			return View(ListVancancyAvailable);
		}

		//
		// GET: /Administrative/Company/Create

		public ActionResult Create( )
		{
			return View( );
		}

		//
		// POST: /Administrative/Company/Create

		[HttpPost]
		public ActionResult Create(Company company)
		{
			if (ModelState.IsValid)
			{
				MembershipCreateStatus createStatus;
				Membership.CreateUser(company.Name, company.Name.Trim() + "IdezJobs", company.Email, null, null, true, null, out createStatus);

				if (createStatus == MembershipCreateStatus.Success)
				{

					_ContextData.Add<Company>(company);
					_ContextData.SaveChanges( );

					Roles.AddUserToRole(company.Name, "Company");
					return RedirectToAction("Index", "Home");



				}
			}
			return View( );
		}

		//
		// GET: /Administrative/Company/Edit/5

		public ActionResult Edit(int id)
		{
			Company CompanyEdit = _ContextData.Get<Company>(id);
			return View(CompanyEdit);
		}

		//
		// POST: /Administrative/Company/Edit/5

		[HttpPost]
		public ActionResult Edit(Company company)
		{
			Company CompanyEdit = _ContextData.Get<Company>(company.Id);
			TryUpdateModel(CompanyEdit);
			_ContextData.SaveChanges( );
			return RedirectToAction("Sucess", "Home");
		}

		//
		// GET: /Administrative/Company/Delete/5

		public ActionResult Delete(int id)
		{
			Company CompanyDelete = _ContextData.Get<Company>(id);
			return View(CompanyDelete);
		}

		//
		// POST: /Administrative/Company/Delete/5

		[HttpPost]
		public ActionResult Delete(Company company)
		{
			Company CompanyDelete = _ContextData.Get<Company>(company.Id);
			_ContextData.Delete<Company>(CompanyDelete);
			_ContextData.SaveChanges( );

			return RedirectToAction("Sucess", "Home");
		}
	}
}
