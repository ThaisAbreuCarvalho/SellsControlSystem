using Microsoft.EntityFrameworkCore;
using Repository.Contexto;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class VendaProdutoRepository : Repository<VendaProduto>, IVendaProdutoRepository
    {
        public VendaProdutoRepository(sistemavendasContext dbContext) : base(dbContext)
        {
        }
    }
}
