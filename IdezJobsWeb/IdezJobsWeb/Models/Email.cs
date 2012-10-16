using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	public class Email
	{
		public virtual long Id { get; set; }

		[Required(ErrorMessage = "O email não pode ser vazio")]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email:")]
		public virtual string EmailUser { get; set; }
		[Display(Name="Assunto:")]
		public virtual string Subject { get; set; }
		public virtual string Body {get; set;}
		[NotMapped]
		public virtual FileUpload Annex { get; set; }
		
	
		
	}
}