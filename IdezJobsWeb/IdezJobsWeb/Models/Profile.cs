using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
	public class Profile
	{
		[Display(Name = "Codigo:")]
		public virtual long Code { get; set; }
		[Display(Name = "Id Linkedin:")]
		public virtual string Id { get; set; }
		[Display(Name = "Primeiro Nome:")]
		public virtual string FirstName { get; set; }
		[Display(Name = "Segundo Nome:")]
		public virtual string LastName { get; set; }
		[Display(Name = "Imagem Publica:")]
		[DataType(DataType.ImageUrl)]
		public virtual string PictureUrl { get; set; }
		[Display(Name = "URL Publica:")]
		[DataType(DataType.Url)]
		public virtual string PublicUrl { get; set; }
		[Display(Name = "Estuda:")]
		public virtual string Headline { get; set; }
		[Display(Name = "Trabalha:")]
		public virtual string Industry { get; set; }
		[Display(Name = "Interesse:")]
		public virtual string Interests { get; set; }
		[Display(Name = "Endereco Email:")]
		public virtual string EmailAddress { get; set; } 		
		[Display(Name = "Id Local do Usuario")]
		public virtual long IdUser { get; set; }







	}
}