using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ClienteViewModel
    {

        public int? Codigo { get; set; }
        [Required(ErrorMessage = "Informe o Nome do Cliente")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o CPF/CNPJ")]
        public string CNPJ_CPF { get; set; }
        [Required(ErrorMessage = "Informe o Email Cliente")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o Celular")]
        public string Celular { get; set; }
    }
}
