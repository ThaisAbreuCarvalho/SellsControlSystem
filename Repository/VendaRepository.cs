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
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        public VendaRepository(sistemavendasContext dbContext) : base(dbContext)
        {
        }

        public override List<Venda> Select(Venda entity)
        {
            return _DbContextSet.Include(x => x.CodclienteNavigation).AsNoTracking().ToList();
        }

        public override Venda Select(int Id)
        {
            return _DbContextSet.Include(x => x.CodclienteNavigation).Include(x => x.Vendaproduto).AsNoTracking().FirstOrDefault();
        }
    }
}
