using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Users;
using Infrastructure.Data.Factories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(SqlConnectionFactory sqlConnection) : base(sqlConnection)
        {
        }
        public override async Task<IEnumerable<Group>> GetAll(string storedProcedure)
        {
            using var connection = _sqlConnection.CreateConnection();
            var groupDictionary = new Dictionary<string, Group>();
            await connection.QueryAsync<Group, User, Group>(
                storedProcedure,
                (group, user) =>
                {
                    if (!groupDictionary.TryGetValue(group.Id, out var currentGroup))
                    {
                        currentGroup = group;
                        currentGroup.Users = new List<User>();
                        groupDictionary.Add(currentGroup.Id, currentGroup);
                    }

                    if (user != null && user.Id != default(string))
                    {
                        currentGroup.Users.Add(user);
                    }

                    return currentGroup;
                },
                CommandType.StoredProcedure,
                splitOn: "Id"
               );
            return groupDictionary.Values;
        }
        public override async Task<Group?> GetById(string storedProcedure, object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            var groupDictionary = new Dictionary<string, Group>();
            var group = await connection.QueryAsync<Group, User, Group>(
                storedProcedure,
                (group, user) =>
                {
                    if (!groupDictionary.TryGetValue(group.Id, out var currentGroup))
                    {
                        currentGroup = group;
                        currentGroup.Users = new List<User>();
                        groupDictionary.Add(currentGroup.Id, currentGroup);
                    }

                    if (user != null && user.Id != default)
                    {
                        currentGroup.Users.Add(user);
                    }

                    return currentGroup;
                },
                param: param,
                commandType: CommandType.StoredProcedure,
                splitOn: "Id"
               );
            return group.FirstOrDefault();
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
