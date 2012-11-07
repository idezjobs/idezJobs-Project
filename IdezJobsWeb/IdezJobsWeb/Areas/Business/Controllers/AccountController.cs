using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IdezJobsWeb.Models;

namespace IdezJobsWeb.Areas.Business.Controllers
{
	[HandleError(View = "Error")]
	[Authorize(Roles = "Company")]
	public class AccountController : Controller
	{
		//
		// GET: /Business/Account/

		public ActionResult Index( )
		{
			return View( );
		}

		public ActionResult LogOff( )
		{
			FormsAuthentication.SignOut( );

			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public ActionResult ChangePassword( )
		{
			return View( );
		}



		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel model)
		{
			if (ModelState.IsValid)
			{


				bool changePasswordSucceeded;
				try
				{
					MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true);
					changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
				}
				catch (Exception)
				{
					changePasswordSucceeded = false;
				}

				if (changePasswordSucceeded)
				{
					return RedirectToAction("ChangePasswordSuccess");
				}
				else
				{
					ModelState.AddModelError("", "A senha atual está incorreta ou a nova senha é inválida.");
				}
			}


			return View(model);
		}



		public ActionResult ChangePasswordSuccess( )
		{
			return View( );
		}

		#region Status Codes
		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for
			// a full list of status codes.
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "Nome de usuário já existe. Digite um nome de usuário diferente.";

				case MembershipCreateStatus.DuplicateEmail:
					return "Um nome de usuário para esse endereço de e-mail já existe. Digite um endereço de e-mail diferente.";

				case MembershipCreateStatus.InvalidPassword:
					return "A senha fornecida é inválida. Por favor, insira um valor de senha válida.";

				case MembershipCreateStatus.InvalidEmail:
					return "O endereço de e-mail fornecido é inválido. Por favor, verifique o valor e tente novamente.";

				case MembershipCreateStatus.InvalidAnswer:
					return "A resposta de recuperação de senha fornecida é inválida. Por favor, verifique o valor e tente novamente.";

				case MembershipCreateStatus.InvalidQuestion:
					return "A questão de recuperação de senha fornecida é inválida. Por favor, verifique o valor e tente novamente.";

				case MembershipCreateStatus.InvalidUserName:
					return "O nome de usuário fornecido é inválido. Por favor, verifique o valor e tente novamente.";

				case MembershipCreateStatus.ProviderError:
					return "O provedor de autenticação retornou um erro. Por favor, verifique a sua entrada e tente novamente. Se o problema persistir, contate o administrador do sistema.";

				case MembershipCreateStatus.UserRejected:
					return "O pedido de criação do usuário foi cancelado. Por favor, verifique a sua entrada e tente novamente. Se o problema persistir, contate o administrador do sistema.";

				default:
					return "Ocorreu um erro desconhecido. Por favor, verifique a sua entrada e tente novamente. Se o problema persistir, contate o administrador do sistema.";
			}
		}
		#endregion

		public ActionResult undefined( )
		{
			return View( );
		}

	}
}
