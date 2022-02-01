using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe seu endereço de email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        public string Senha { get; set; }
    }

    //auxiliar

    public class UserSessionInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
}
