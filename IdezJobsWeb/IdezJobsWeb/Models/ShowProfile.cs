using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	//Visualizar Perfil 
	public class ShowProfile
	{
		public virtual long Id { get; set; }
		[Display(Name = "Nome:")]
		public virtual string Name { get; set; }
		[Display(Name = "Sobrenome:")]
		public virtual string Surname { get; set; }
		[Display(Name = "Email:"), DataType(DataType.EmailAddress)]
		public virtual string Email { get; set; }

	}
}