using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public partial class Produto
    {
        public Produto()
        {
            Vendaproduto = new HashSet<VendaProduto>();
        }

        [Key]
        public int? Codigo { get; set; }
        [ForeignKey("CodCategoria")]
        public int? Codcategoria { get; set; }
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }

        public virtual Categoria CodcategoriaNavigation { get; set; }
        public virtual ICollection<VendaProduto> Vendaproduto { get; set; }
    }
}
