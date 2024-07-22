using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id); 
        Task<T> CreateAsync (T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
