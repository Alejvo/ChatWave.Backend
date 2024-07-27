using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITokenRepository:IRepository<Token>
    {
        Task<Token?> GetToken(string token);
        Task SaveToken(Token token);
    }
}
