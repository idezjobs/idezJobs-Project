using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	//Vaga 
	public class Vacancy
	{
		public virtual long Id { get; set; }

		[Required(ErrorMessage = "A data de cadastro não pode ser vazia")]
		[Display(Name = "Data Cadastro:")]
		public virtual DateTime RegistrionDate { get; set; }

		//data limite cadastro
		[Required(ErrorMessage = "A data limite cadastro da vaga não pode ser vazia")]
		[Display(Name = "Data Limite Cadastro:")]

		public virtual DateTime RegistrationDeadline { get; set; }

		[Required(ErrorMessage = "A descrição não pode ser vazia"), StringLength(200, ErrorMessage = "A descrição não pode ter mais de cinquenta caracteres", MinimumLength = 6)]
		[Display(Name = "Descrição:")]
		public virtual string Description { get; set; }

		[Required(ErrorMessage = "O nome da empresa não pode ser vazio"), StringLength(50, ErrorMessage = "O nome da empresa não pode ter mais de cinquenta caracteres", MinimumLength = 3)]
		[Display(Name = "Nome Empresa:")]
		public virtual string CompanyName { get; set; }

		[Required(ErrorMessage = "O número de vagas não pode ser vazio"), Range(1, 9999, ErrorMessage = "O número de vagas deve ir de 1 a 9999")]
		[Display(Name = "Número de Vagas:")]
		public virtual int JobsNumber { get; set; }

		//perfil  da vagas
		[Display(Name = "Perfil da Vaga:")]
		public virtual ProfileVacancy ProfileVacancy { get; set; }
		[Display(Name = "Status:")]
		[Required(ErrorMessage = "O Status da vaga não pode ser vazio")]
		public virtual Status Status { get; set; }



		
	}

}