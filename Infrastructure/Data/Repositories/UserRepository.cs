using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
using Infrastructure.Data.Factories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(SqlConnectionFactory sqlConnection) : base(sqlConnection)
        {
        }

        public async Task<User?> GetByUserName(string username)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                UserProcedures.GetUserByUsername,
                new {username},
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            var users = await GetAll(UserProcedures.GetUsers);
            if(users.Any(x=>x.Email  == email)) return false;
            return true;
        }

        public async Task<bool> IsUserNameUnique(string username)
        {
            var users = await GetAll(UserProcedures.GetUsers);
            if (users.Any(x => x.UserName == username)) return false;
            return true;
        }

        public async Task<User> LoginUser(string email, string password)
        {
            using var connection = _sqlConnection.CreateConnection();
            var user = await connection.QuerySingleOrDefaultAsync<User>(
                UserProcedures.LoginUser,
                new { email,password},
                commandType: CommandType.StoredProcedure);
            if (user != null) return user;
            return default;
        }
    }
}
