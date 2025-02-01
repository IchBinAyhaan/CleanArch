using CleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Repositories
{
    public interface IProductReadRepository : IBaseReadRepository<Product>
    {
        Task<Product> GetByNameAsync(string name);
    }
}
