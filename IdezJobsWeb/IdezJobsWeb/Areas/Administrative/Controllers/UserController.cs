using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdezJobsWeb.Models;
using IdezJobsWeb.Models.Context;
using System.Web.Security;

namespace IdezJobsWeb.Areas.Administrative.Controllers
{
	[HandleError(View = "Error")]
	[Authorize(Roles = "Administrador")]
	public class UserController : Controller
	{
		private IContextData _ContextData = new ContextDataNH( );
		//
		// GET: /Administrative/User/

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
			user.DateRegister = DateTime.Now;

			user.Token = user.Name.GetHashCode( ).ToString( );
			user.Type = "Administrador";
			if (ModelState.IsValid)
			{

				MembershipCreateStatus createStatus;
				Membership.CreateUser(user.Name, user.Name.Trim( ) + "idezjobs", user.Email, null, null, true, null, out createStatus);
				if (createStatus == MembershipCreateStatus.Success)
				{
					_ContextData.Add<User>(user);
					_ContextData.SaveChanges( );

					Roles.AddUserToRole(user.Name, user.Type);

					return RedirectToAction("Sucess", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Não foi possível criar o usuário");
				}
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
