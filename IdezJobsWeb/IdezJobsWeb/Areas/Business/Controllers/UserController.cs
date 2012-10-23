using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models.Context;
using IdezJobsWeb.Models;


namespace IdezJobsWeb.Areas.Business.Controllers
{
	[HandleError(View = "Error")]
	[Authorize(Roles = "Company")]
	public class UserController : Controller
	{
		private IContextData _ContextData = new ContextDataNH( );
		// GET: /Business/User/

		public ActionResult Index( )
		{
			return View( );
		}
		//
		// GET: /Administrative/User/Details/5

		public ActionResult Details(int id)
		{
			User UserDetails = _ContextData.Get<User>(id);
			return View(UserDetails);
		}

		public ActionResult ListDetails( )
		{
			IList<User> UserList = null;
			UserList = _ContextData.GetAll<User>( ).ToList( );
			return View(UserList);
		}

		//
		// GET: /Administrative/User/Create

		public ActionResult Create( )
		{
			return View( );
		}

		//
		// POST: /Administrative/User/Create

		[HttpPost]
		public ActionResult Create(User user)
		{
			if (ModelState.IsValid)
			{
				_ContextData.Add<User>(user);
				_ContextData.SaveChanges( );
				return RedirectToAction("Sucess", "Home");

			}
			return View( );
		}

		//
		// GET: /Administrative/User/Edit/5

		public ActionResult Edit(int id)
		{
			User UserEdit = _ContextData.Get<User>(id);
			return View(UserEdit);
		}

		//
		// POST: /Administrative/User/Edit/5

		[HttpPost]
		public ActionResult Edit(User userEdit)
		{
			User UserEdi = _ContextData.Get<User>(userEdit.Id);
			TryUpdateModel(UserEdi);
			_ContextData.SaveChanges( );
			return RedirectToAction("Sucess", "Home");


		}

		//
		// GET: /Administrative/User/Delete/5

		public ActionResult Delete(int id)
		{
			User UserDelete = _ContextData.Get<User>(id);

			return View(UserDelete);
		}

		//
		// POST: /Administrative/User/Delete/5

		[HttpPost]
		public ActionResult Delete(User userDelete)
		{
			User UserDel = _ContextData.Get<User>(userDelete.Id);
			_ContextData.Delete<User>(UserDel);
			_ContextData.SaveChanges( );
			return RedirectToAction("Sucess", "Home");
		}
	}
}
