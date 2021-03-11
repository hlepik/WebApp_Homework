using System;
using System.Threading.Tasks;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class BaseUnitOfWork : IBaseUnitOfWork
    {
        public abstract Task<int> SaveChangesAsync();
    }

}
