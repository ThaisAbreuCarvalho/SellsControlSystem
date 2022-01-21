using System;
using System.Collections.Generic;

namespace SistemaVenda.Entities
{
    public partial class Categoria
    {
        public Categoria()
        {
            Produto = new HashSet<Produto>();
        }

        public int Codigo { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Produto> Produto { get; set; }
    }
}
