using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entities
{
    public class VendaProduto
    {
        [Key]
        public int CodigoVenda { get; set; }

        public int CodigoProduto { get; set; }

        public double Quantidade { get; set; }

        public decimal Total { get; set; }
        public decimal ValorUnitario { get; set; }
        
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }

    }
}
