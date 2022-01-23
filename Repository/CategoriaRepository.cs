using Microsoft.EntityFrameworkCore;
using Repository.Contexto;
using Repository.Entities;
using Repository.Interfaces;
using System.Collections.Generic;

namespace Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(sistemavendasContext dbContext) : base(dbContext)
        {
        }
    }
}
