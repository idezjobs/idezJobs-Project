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

		[Required(ErrorMessage = "A descrição não pode ser vazia"), StringLength(255, ErrorMessage = "A descrição não pode ter mais de 255 caracteres", MinimumLength = 6)]
		[Display(Name = "Requisitos:")]
		public virtual string Description { get; set; }	

		[Display(Name = "Nome Empresa:")]
		public virtual Company CompanyName { get; set; }

		[Required(ErrorMessage = "O horário de trabalho não pode ser vazio"), StringLength(255, ErrorMessage = "O nome da empresa não pode ter mais de 255  caracteres", MinimumLength = 3)]
		[Display(Name = "Horário de Trabalho:")]
		public virtual string OfficeHours { get; set; }

		[Display(Name = "Benefícios:")]
		public virtual string Benefits { get; set; } 

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