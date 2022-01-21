using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe seu endereço de email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        public string Senha { get; set; }
    }
}
