using Repository.Contexto;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UserRepository : Repository<Usuario>, IUserRepository
    {
        public UserRepository(sistemavendasContext dbContext) : base(dbContext)
        {
        }
    }
}
