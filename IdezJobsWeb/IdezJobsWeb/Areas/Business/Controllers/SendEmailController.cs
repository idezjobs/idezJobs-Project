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
	public class SendEmailController : MailerBase
	{
		public EmailResult EmailParaFaculdade(List<User> list)
		{
			foreach (var item in list)
			{
				From = "IdezJobs@gmail.com";
				To.Add(item.Email);
				Subject = "Assunto Teste";
				

			}

			return Email("EmailParaFaculdade");
		}


	}
}
