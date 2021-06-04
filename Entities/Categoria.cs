using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entities
{
    public class Categoria
    {
        [Key]
        public int? Codigo { get; set; }

        public string Descricao { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
