using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entities
{
    public partial class VendaProduto
    {
        [Key]
        public int? Codigo { get; set; }
        public int? Codigoproduto { get; set; }
        public int? Codigovenda { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Total { get; set; }

        public virtual Produto CodigoprodutoNavigation { get; set; }
        public virtual Venda CodigovendaNavigation { get; set; }
    }
}
