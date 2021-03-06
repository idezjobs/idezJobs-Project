﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IdezJobsWeb.Models;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth.ChannelElements;
using System.IO;
using System.Web.Services;
using IdezJobsWeb.Models.Context;
using System.Web.Script.Serialization;


namespace IdezJobsWeb.Controllers
{
	public class AccountController : Controller
	{
		private IContextData _ContextoAccount = new ContextDataNH( );

		[HttpPost]
		public ActionResult saveUser(string id, string firstName, string lastName, string pictureUrl, string publicProfileUrl, string headline, string industry, string interests, string emailAddress)
		{
			User usuario = new Models.User( );
			if (interests == null || interests == "undefined")
			{
				interests = "sem interesse";
			}
			else if (pictureUrl == null || pictureUrl == "undefined")
			{
				pictureUrl = "sem imagem";
			}
			else if (headline == null || headline == "undefined")
			{
				headline = "sem informação de estudos";
			}
			if (industry == null || industry == "undefined")
			{
				industry = "no momento nao trabalha";
			}
			MembershipUser UsuarioExiste = FindUserByEmail(emailAddress);

			var idBase = _ContextoAccount.GetAll<Profile>( )
						  .Where(x => x.Id == id).ToList( );

			if (idBase.Count( ) >= 1)
			{
				Profile pUpdate = _ContextoAccount.GetAll<Profile>( )
								  .Where(x => x.Id == id).First( );
				pUpdate.Id = id;
				pUpdate.FirstName = firstName;
				pUpdate.LastName = lastName;
				pUpdate.PictureUrl = pictureUrl;
				pUpdate.PublicUrl = publicProfileUrl;
				pUpdate.Headline = headline.ToLower( );
				pUpdate.Industry = industry.ToLower( );
				pUpdate.Interests = interests.ToLower( );
				pUpdate.EmailAddress = emailAddress;
				pUpdate.IdUser = (from c in _ContextoAccount.GetAll<User>( )
								 .Where(x => x.Token == id)
								  select c.Id).First( );
				pUpdate.KeyWords = RemoveString.GenerateKeyWords(interests);

				TryUpdateModel(pUpdate);
				_ContextoAccount.SaveChanges( );

				if (Membership.ValidateUser(pUpdate.FirstName, pUpdate.FirstName + pUpdate.Id))
				{
					FormsAuthentication.SetAuthCookie(pUpdate.FirstName, false);
					return RedirectToAction("ListVacancyOpen", "Vacancy", new { area = "CommonUser" });
				}


				return Json(new { codigo = 1, msg = "Dados atualizados com sucesso!" });
			}
			else
			{

				usuario.DateRegister = DateTime.Now;
				usuario.Email = emailAddress;
				usuario.Name = firstName;
				usuario.Type = "Student";
				usuario.Token = id;

				_ContextoAccount.Add<User>(usuario);
				_ContextoAccount.SaveChanges( );

				Profile p = new Profile( );
				p.Id = id;
				p.FirstName = firstName;
				p.LastName = lastName;
				p.PictureUrl = pictureUrl;
				p.PublicUrl = publicProfileUrl;
				p.Headline = headline.ToLower( );
				p.Industry = industry.ToLower( );
				p.Interests = interests.ToLower( );
				p.EmailAddress = emailAddress;
				p.IdUser = (from c in _ContextoAccount.GetAll<User>( )
							 .Where(x => x.Token == id)
							select c.Id).First( );
				p.KeyWords = RemoveString.GenerateKeyWords(interests);


				MembershipCreateStatus status;
				Membership.CreateUser(p.FirstName, p.FirstName + p.Id, p.EmailAddress, null, null, true, out status);
				if (status == MembershipCreateStatus.Success)
				{
					Roles.AddUserToRole(firstName, "Student");

					_ContextoAccount.Add<Profile>(p);
					_ContextoAccount.SaveChanges( );


					if (Membership.ValidateUser(p.FirstName, p.FirstName + p.Id))
					{
						FormsAuthentication.SetAuthCookie(p.FirstName, false);
						return RedirectToAction("Index", "Home", new { area = "CommonUser" });
					}

				}


				return Json(new { codigo = 1, msg = "Dados inseridos com sucesso!" });

			}
		}

		MembershipUser FindUserByEmail(string email)
		{
			MembershipUserCollection members = Membership.FindUsersByEmail(email);

			if (members.Count > 0)
			{
				foreach (MembershipUser member in members)
				{
					return member;
				}
			}

			return null;
		}

		public ActionResult LogOn( )
		{
			return View( );
		}


		[HttpPost]
		public ActionResult LogOn(UserCollege model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(model.Name, model.Password))
				{
					FormsAuthentication.SetAuthCookie(model.Name, model.RememberMe);
					if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
						&& !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "O nome do usuário ou a senha estão incorretos.");
				}
			}


			return View(model);
		}



		public ActionResult LogOff( )
		{
			FormsAuthentication.SignOut( );

			return RedirectToAction("Index", "Home");
		}



		public ActionResult Register( )
		{
			return View( );
		}



		[HttpPost]
		public ActionResult Register(RegisterUserCollege model)
		{
			if (ModelState.IsValid)
			{

				MembershipCreateStatus createStatus;
				Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

				if (createStatus == MembershipCreateStatus.Success)
				{
					FormsAuthentication.SetAuthCookie(model.UserName, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", ErrorCodeToString(createStatus));
				}
			}


			return View(model);
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
