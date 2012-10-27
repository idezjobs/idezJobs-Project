using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	//Empresa
	public class Company
	{
		public virtual long Id { get; set; }

		[Required(ErrorMessage = "O nome não pode ser vazio"), StringLength(50, ErrorMessage = "O nome da empresa não pode ter mais de cinquenta caracteres")]
		[Display(Name = "Nome Empresa:")]
		public virtual string Name { get; set; }

		[Required(ErrorMessage = "O email não pode ser vazio"), DataType(DataType.EmailAddress)]
		[Display(Name = "Email:")]
		public virtual string Email { get; set; }

		[Display(Name = "Endereço:"), StringLength(100, ErrorMessage = "O endereço não pode ter mais de cem caracteres")]
		public virtual string Address { get; set; }

		[Display(Name = "Bairro:"), StringLength(50, ErrorMessage = "O bairro não pode ter mais de cinquenta caracteres")]
		public virtual string Neighborhood { get; set; }

		[Display(Name = "Cidade:"), StringLength(50, ErrorMessage = "O nome da cidade não pode ter mais de cinquenta caracteres ")]
		public virtual string City { get; set; }

		[Display(Name = "Estado:"), StringLength(50, ErrorMessage = "O nome do estado não pode ter mais de cinquenta caracteres")]
		public virtual string State { get; set; }

		[Display(Name = "Area de Atuacao"), StringLength(50, ErrorMessage = "A area de atuação não pode ter mais de cinquenta caracteres")]	  
		public virtual string Operation { get; set; }

		public virtual IList<Vacancy> ListVacancyBusiness { get; set; }






	}
}