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

        public async Task<User?> GetByUserName(string storedProcedure,object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                storedProcedure,
                param,
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


    }
}
