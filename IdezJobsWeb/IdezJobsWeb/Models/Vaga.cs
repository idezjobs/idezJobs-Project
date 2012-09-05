using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
    public class Vaga
    {
        public virtual long Id { get; set; }
        [Required(ErrorMessage = "A data de cadastro não pode ser vazia")]
        public virtual DateTime DataCadastro { get; set; }
        public virtual DateTime DataLimiteCadastro { get; set; }  
        [Required(ErrorMessage="A descrição não pode ser vazia")]
        public virtual string Descricao { get; set; }
        [Required(ErrorMessage="O nome da empresa não pode ser vazio")]
        public virtual string NomeEmpresa { get; set; }
        [Required(ErrorMessage="O número de vagas não pode ser vazio")]
        public virtual int NumeroVagas { get; set; }
        public virtual string PerfilVagas { get; set; }

    }
}