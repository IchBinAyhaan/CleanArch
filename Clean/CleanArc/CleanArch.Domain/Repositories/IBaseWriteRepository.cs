using CleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Repositories
{
    public interface IBaseWriteRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T data);
        void Update(T data);
        void Delete(T data);
    }
}
