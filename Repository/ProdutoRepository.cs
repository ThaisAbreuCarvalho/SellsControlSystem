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
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(sistemavendasContext dbContext) : base(dbContext)
        {
        }

        public override List<Produto> Select(Produto entity)
        {
            return _DbContextSet.Include(x=> x.CodcategoriaNavigation).AsNoTracking().ToList();
        }

        public override Produto Select(int Id)
        {
            var products = Select(new Produto { });
            return products.Find(x=> x.Codigo == Id);
        }
    }
}
