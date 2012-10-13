using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	public class ProfileVacancy
	{
		public virtual long Id { get; set; }
		[Required(ErrorMessage = "A descrição do perfil não pode ser vazia"), StringLength(60, ErrorMessage = "A descrição não pode ter mais de sessenta(60) caracteres")]
		[Display(Name = "Perfil da Vaga:")]
		public virtual string Myprofile { get; set; }
	}
}