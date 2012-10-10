using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	public class StatusVacancy
	{
		public virtual long Id { get; set; }
		
		[Required(ErrorMessage = "O status da vaga não pode ser vazio"), StringLength(55, ErrorMessage = "O status da vaga não pode ter mais que cinquenta caracteres", MinimumLength = 3)]
		[Display(Name = "Status da Vaga:")]
		public virtual string Status { get; set; }
	}
}