using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public partial class Venda
    {
        public Venda()
        {
            Vendaproduto = new HashSet<VendaProduto>();
        }

        [Key]
        public int Codigo { get; set; }
        [ForeignKey("codcliente")]
        public int? Codcliente { get; set; }
        public DateTime? Data { get; set; }
        public decimal? Total { get; set; }

        public virtual Cliente CodclienteNavigation { get; set; }
        public virtual ICollection<VendaProduto> Vendaproduto { get; set; }
    }
}
