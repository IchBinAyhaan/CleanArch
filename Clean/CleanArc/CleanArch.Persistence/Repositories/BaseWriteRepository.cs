using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Persistence.Repositories
{
    public class BaseWriteRepository<T> : IBaseWriteRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _table;

        public BaseWriteRepository(AppDbContext context)
        {
            _table = context.Set<T>();
        }
        public async Task CreateAsync(T data)
        {
            await _table.AddAsync(data);
        }
        public void Update(T data)
        {
            _table.Update(data);
        }
        public void Delete(T data)
        {
            _table.Remove(data);
        }
    }
}
