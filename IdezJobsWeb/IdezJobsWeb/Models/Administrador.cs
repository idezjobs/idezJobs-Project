using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
    public class Administrador
    {
        public virtual long Id { get; set; }
        [Required(ErrorMessage="O nome não pode ser vazio")]
        public virtual string Nome { get; set; }
        [Required(ErrorMessage="O email não pode ser vazio")]
        public virtual string Email { get; set; }
        [Required(ErrorMessage="A senha não pode ser vazia")]
        public virtual string Senha { get; set; }
        [Required(ErrorMessage="O tipo não pode ser vazio")]
        public virtual string Tipo { get; set; }

    }
}