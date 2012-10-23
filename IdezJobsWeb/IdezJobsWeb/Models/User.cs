using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IdezJobsWeb.Models
{
	//Usuário
	public class User
	{
		public virtual long Id { get; set; }

		[Required(ErrorMessage = "O nome não pode ser vazio"), StringLength(50, ErrorMessage = "O nome não pode ter mais de cinquenta caracteres")]
		[Display(Name = "Nome:")]
		public virtual string Name { get; set; }

		[Required(ErrorMessage = "O tipo não pode ser vazio"), StringLength(50, ErrorMessage = "O tipo não pode ter mais de cinquenta caracteres", MinimumLength = 3)]
		[Display(Name = "Tipo Usuário:")]
		public virtual string Type { get; set; }

		[Required(ErrorMessage = "O email não pode ser vazio"), DataType(DataType.EmailAddress)]
		[Display(Name = "Email:")]
		public virtual string Email { get; set; }

		[Display(Name = "Data Registro:")]
		[DataType(DataType.DateTime)]
		public virtual DateTime DateRegister { get; set; }

		[Display(Name = "Token:")]
		public virtual string Token { get; set; }
	}


	public class UserCollege
	{
		[Required(ErrorMessage = "O nome é obrigatório")]
		[Display(Name = "Nome:")]
		public virtual string Name { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória"), DataType(DataType.Password)]
		[Display(Name = "Senha:")]
		public virtual string Password { get; set; }

		[Display(Name = "Lembrar ?")]
		public virtual bool RememberMe { get; set; }
	}

	public class RegisterUserCollege
	{
		[Required(ErrorMessage = "O nome não pode ser vazio")]
		[Display(Name = "Nome:")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "O email não pode ser vazio")]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email:")]
		public string Email { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória")]
		[StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Senha:")]
		public string Password { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória")]
		[StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Confirmar Senha:")]
		public string ConfirmPassword { get; set; }
	}

	public class ChangePasswordModel
	{
		[Required(ErrorMessage = "A senha é obrigatória")]
		[StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Senha Atual:")]
		public string OldPassword { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória")]
		[StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Nova Senha:")]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "A senha é obrigatória")]
		[StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação de senha não conferem.")]
		[Display(Name = "Confirmar Senha:")]
		public string ConfirmPassword { get; set; }
	}
}