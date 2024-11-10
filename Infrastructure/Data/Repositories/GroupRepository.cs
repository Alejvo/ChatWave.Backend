using Dapper;
using Domain.Interfaces;
using Domain.Models.Groups;
using Domain.Models.Users;
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
    public class GroupRepository : Repository<GroupResponse>, IGroupRepository
    {
        public GroupRepository(SqlConnectionFactory sqlConnection) : base(sqlConnection)
        {
        }
        public override async Task<IEnumerable<GroupResponse>> GetAll(string storedProcedure)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryAsync<GroupResponse>(storedProcedure);
        }
        public override async Task<GroupResponse?> GetById(string storedProcedure, object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GroupResponse>
                (
                    storedProcedure,
                    param
                );
        }

        public async Task<IEnumerable<GroupResponse>> GetByNames(string name)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryAsync<GroupResponse>
                (
                    GroupProcedures.GetGroupsName,
                    new {name}
                );
        }
        public async Task AddUserToGroup(string storedProcedure, string groupId, string userId)
        {
            using var connection = _sqlConnection.CreateConnection();
            await connection.ExecuteAsync(
                storedProcedure, 
                new {groupId,userId},
                commandType:CommandType.StoredProcedure);

        }

        public async Task RemoveUserToGroup(string storedProcedure, string groupId, string userId)
        {
            using var connection = _sqlConnection.CreateConnection();
            await connection.ExecuteAsync(
                storedProcedure,
                new { groupId, userId },
                commandType: CommandType.StoredProcedure);

        }
    }
}
