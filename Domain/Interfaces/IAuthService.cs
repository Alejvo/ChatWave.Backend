﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string userId,string username);
        string GenerateRefreshToken();
        Task SaveRefreshToken(string userId, string refreshToken);
    }
}
