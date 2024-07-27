using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
using Infrastructure.Data.Factories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TokenRepository :Repository<Token>,ITokenRepository
    {
        public TokenRepository(SqlConnectionFactory sqlConnection) : base(sqlConnection)
        {
        }

        public async Task<Token?> GetToken(string token)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Token>(
                TokenProcedures.GetToken,
                new {token},
                commandType:CommandType.StoredProcedure);
        }

        public async Task SaveToken(Token token)
        {
            using var connection = _sqlConnection.CreateConnection();
            await connection.ExecuteAsync(
                TokenProcedures.SaveToken,
                new {token.UserId,Token = token.JwtToken,token.Id,token.ExpiryTime},
                commandType:CommandType.StoredProcedure);
        }
    }
}
