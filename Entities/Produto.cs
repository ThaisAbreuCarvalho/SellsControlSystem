using System;
using System.Collections.Generic;

namespace SistemaVenda.Entities
{
    public partial class Produto
    {
        public Produto()
        {
            Vendaproduto = new HashSet<Vendaproduto>();
        }

        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public int Codcategoria { get; set; }

        public virtual Categoria CodcategoriaNavigation { get; set; }
        public virtual ICollection<Vendaproduto> Vendaproduto { get; set; }
    }
}
