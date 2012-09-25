using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	public class Administrator
	{
		public virtual long Id { get; set; }

		[Required(ErrorMessage = "O nome não pode ser vazio"), StringLength(50, ErrorMessage = "O nome não pode ter mais de cinquenta caracteres", MinimumLength = 4)]
		[Display(Name = "Nome:")]
		public virtual string Name { get; set; }

		[Required(ErrorMessage = "O email não pode ser vazio"), DataType(DataType.EmailAddress)]
		[Display(Name = "Email:")]
		public virtual string Email { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória")]
		[StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Senha:")]
		public virtual string Password { get; set; }

		[Required(ErrorMessage = "O tipo não pode ser vazio"), StringLength(50, ErrorMessage = "O tipo não pode ter mais de cinquenta caracteres", MinimumLength = 3)]
		public virtual string Type { get; set; }
	}
}