using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class Repository<TEntidade> : DbContext, IRepository<TEntidade>
        where TEntidade : class, new()
    {
        DbSet<TEntidade> _DbContextSet;
        DbContext _Db;

        public Repository(DbContext dbContext)
        {
            _Db = dbContext;
            _DbContextSet = _Db.Set<TEntidade>();
        }

        public void Delete(int Id)
        {
            var entity = Select(Id);
            _DbContextSet.Remove(entity);
            _Db.SaveChanges();
        }

        public void Insert(TEntidade entity)
        {
            _DbContextSet.Add(entity);
            _Db.SaveChanges();
        }

        public void Insert(List<TEntidade> entities)
        {
            _DbContextSet.AddRange(entities);
            _Db.SaveChanges();
        }

        public List<TEntidade> Select(TEntidade entity)
        {
            return _DbContextSet.AsNoTracking().ToList();
        }

        public TEntidade Select(int Id)
        {
            return _DbContextSet.Find(Id);
        }
    }
}
