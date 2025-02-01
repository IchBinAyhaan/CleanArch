using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using CleanArch.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Persistence.Repositories
{
    public class ProductReadRepository : BaseReadRepository<Product>, IProductReadRepository
    {
        private readonly AppDbContext _context;

        public ProductReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        }
    }
}
