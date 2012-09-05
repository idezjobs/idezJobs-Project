﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdezJobsWeb.Models
{
    public class Usuario
    {
        public virtual long Id { get; set; }
        [Required(ErrorMessage="O nome não pode ser vazio")]
        public virtual string Nome { get; set; }
        [Required(ErrorMessage="O tipo não pode ser vazio")]
        public virtual string Tipo { get; set; }
        public virtual string Token { get; set; }

    }
}