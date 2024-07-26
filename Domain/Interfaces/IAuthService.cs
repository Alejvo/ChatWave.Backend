using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string userId);
        string GenerateRefreshToken();
        Task SaveRefreshToken(string userId, string refreshToken);
        Task<bool> ValidateRefreshToken(string userId, string refreshToken);
    }
}
