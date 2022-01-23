using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstraction
{
    public interface IRepository<TEntidade>
        where TEntidade : class
    {
        void Insert(TEntidade entity);
        void Insert(List<TEntidade> entities);
        List<TEntidade> Select(TEntidade entity);
        TEntidade Select(int Id);
        void Delete(int Id);
    }
}
