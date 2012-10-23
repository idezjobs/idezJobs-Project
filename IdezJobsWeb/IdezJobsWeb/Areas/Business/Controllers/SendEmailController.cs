using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net;
using ActionMailer.Net.Mvc;
using IdezJobsWeb.Models;

namespace IdezJobsWeb.Areas.Business.Controllers
{
	[HandleError(View = "Error")]
	[Authorize(Roles = "Company")]
	public class SendEmailController : MailerBase
	{
		public EmailResult EmailParaFaculdade(List<User> list)
		{
			foreach (var item in list)
			{
				From = "IdezJobs@gmail.com";
				To.Add(item.Email);
				Subject = "Seja Bem Vindo ";

				@ViewBag.Nome = item.Name;

				@ViewBag.nu = list.Count;

				return Email("EmailParaFaculdade", list);
			}

			return Email("EmailParaFaculdade", list);
		}

		public EmailResult EmailParaCandidato(List<User> list)
		{
			return Email("EmailParaCandidato", list);
		}


	}
}
