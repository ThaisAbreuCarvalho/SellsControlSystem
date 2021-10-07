using System;
using System.Collections.Generic;

namespace SistemaVenda.Entities
{
    public partial class Vendaproduto
    {
        public int? Codigo { get; set; }
        public int? Codigoproduto { get; set; }
        public int? Codigovenda { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Total { get; set; }

        public virtual Produto CodigoprodutoNavigation { get; set; }
        public virtual Venda CodigovendaNavigation { get; set; }
    }
}
