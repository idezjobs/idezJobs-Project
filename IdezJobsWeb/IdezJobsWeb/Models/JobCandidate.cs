using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	//Candidato Vaga
	public class JobCandidate
	{
		
	
		public virtual long Id { get; set; }

		[Required(ErrorMessage = "Descreva alguma informação adicional"), StringLength(100, ErrorMessage = "A descrição não pode ter mais de cem caracteres", MinimumLength = 10)]
		[Display(Name = "Informação Adicional:")]
		public virtual string AdditionalInformation { get; set; }

		[Required(ErrorMessage = "A vaga não pode ser vazia")]
		[Display(Name = "Descrição da Vaga:")]
		public virtual Vacancy JobCandidato { get; set; }

		[Required(ErrorMessage = "O Usuário não pode ser vazio")]
		[Display(Name = "Nome Candidato:")]
		public virtual User UserJobs { get; set; }
	}
}