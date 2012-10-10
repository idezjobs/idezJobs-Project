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
		public EmailResult EmailParaFaculdade( )
		{
			From = "Ademivieira@gmail.com";
			To.Add("IdezJobs@gmail.com");
			Subject = "Assunto Teste";
			return Email("Email enviado com sucesso");

		}
	}
}
