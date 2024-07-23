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
        Task<IEnumerable<T>> GetAll(string storedProcedure);
        Task<T?> GetById(string storedProcedure,object id); 
        Task CreateAsync (string storedProcedure,object param);
        Task UpdateAsync(string storedProcedure, object param);
        Task DeleteAsync(string storedProcedure, object param);
    }
}
