using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryPatternDemo.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        Task<bool> SaveAsync();
    }
}
