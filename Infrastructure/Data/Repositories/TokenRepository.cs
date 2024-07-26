using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public Task<bool> IsRefreshTokenValid(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task SaveToken(string userId, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
