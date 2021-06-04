using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entities
{
    public class Cliente
    {
        [Key]
        public int? Codigo { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string CNPJ_CPF { get; set; }

        public string Celular { get; set; }

        public ICollection<Venda> Vendas { get; set; }
    }
}
