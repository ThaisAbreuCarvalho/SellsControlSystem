using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entities
{
    public partial class Produto
    {
        public Produto()
        {
            Vendaproduto = new HashSet<Vendaproduto>();
        }

        [Key]
        public int? Codigo { get; set; }
        public int? Codcategoria { get; set; }
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }

        public virtual Categoria CodcategoriaNavigation { get; set; }
        public virtual ICollection<Vendaproduto> Vendaproduto { get; set; }
    }
}
