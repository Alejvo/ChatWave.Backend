using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, 
            ITokenRepository tokenRepository, 
            IUserRepository userRepository)
        {
            _configuration = configuration;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        public string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            string refreshToken;
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(byteArray);
            refreshToken = Convert.ToBase64String(byteArray);
            return refreshToken;
        }

        public string GenerateToken(string userId)
        {
            var key = _configuration.GetValue<string>("JwtSettings:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }



        public async Task SaveRefreshToken(string userId, string refreshToken)
        {
            var user = await _userRepository.GetById(UserProcedures.GetUserById, new {Id= userId});
            if(user != null)
            {
                var token = new Token
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = user.Id,
                    ExpiryTime = DateTime.UtcNow.AddMinutes(15),
                    JwtToken = refreshToken
                };
                await _tokenRepository.SaveToken(token);
            }
        }

    }
}
