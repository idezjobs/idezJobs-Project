using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
    public class CandidatoVaga
    {
        public virtual long Id { get; set; }
        [Required(ErrorMessage="Descreva alguma informação adicional")]
        public virtual string InformacoesAdicionais { get; set; }
        [Required(ErrorMessage="A vaga não pode ser vazia")]
        public virtual Vaga VagaCandidato { get; set; }
        [Required(ErrorMessage="O Usuário não pode ser vazio")]
        public virtual Usuario UsuarioDaVaga { get; set; }
    }
}