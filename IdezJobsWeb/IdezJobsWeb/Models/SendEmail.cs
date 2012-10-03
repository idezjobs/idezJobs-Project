using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	public class SendEmail
	{
		public virtual long Id { get; set; }
		[Required(ErrorMessage="O nome não pode ser vazio"), StringLength(50, ErrorMessage = "O nome não pode ter mais de cinquenta caracteres")]
		[Display(Name="Nome:")]
		public virtual string NameSendEmail { get; set; }
		public virtual IList<Email> EmailList { get; set; }
	}
}