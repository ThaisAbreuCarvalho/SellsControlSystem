using System;
using System.Collections.Generic;

namespace SistemaVenda.Entities
{
    public partial class Usuario
    {
        public int Codigo { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
