using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Messages;
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

        public async override Task<IEnumerable<User>> GetAll(string storedProcedure)
        {
           using var connection = _sqlConnection.CreateConnection();
            var userDictionary = new Dictionary<string, User>();
            await connection.QueryAsync<User, Group, User, User>(
                storedProcedure,
                (user, group, friend) =>
                {
                    if (!userDictionary.TryGetValue(user.Id, out var userEntry))
                    {
                        userEntry = user;
                        userEntry.Groups = new List<string>();
                        userEntry.Friends = new List<Friend>();
                        userDictionary.Add(userEntry.Id, userEntry);
                    }
                    if (group.Name != null && !userEntry.Groups.Any(g => g == group.Name))
                    {
                        userEntry.Groups.Add(group.Name);
                    }
                    if (friend.UserName != null && !userEntry.Friends.Any(f=>f.Name ==friend.UserName))
                    {
                        var newFriend = new Friend
                        {
                            Id = friend.Id,
                            Name = friend.UserName
                        };
                        userEntry.Friends.Add(newFriend);
                    }
                    return userEntry;
                },
                commandType: CommandType.StoredProcedure,
                splitOn: "GroupId,Id"
                );
            return userDictionary.Values;
        }

        public override async Task<User?> GetById(string storedProcedure, object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            var userDictionary = new Dictionary<string,User>();
            await connection.QueryAsync<User,Group,User,User>(
                storedProcedure,
                (user,group, friend) =>
                {
                    if(!userDictionary.TryGetValue(user.Id,out var userEntry))
                    {
                        userEntry = user;
                        userEntry.Groups = new List<string>();
                        userEntry.Friends = new List<Friend>();
                        userDictionary.Add(userEntry.Id,userEntry);
                    }
                    if(group.Name != null && !userEntry.Groups.Any(g => g == group.Name))
                    {
                        userEntry.Groups.Add(group.Name);
                    }
                    if (friend.UserName != null && !userEntry.Friends.Any(f => f.Name == friend.UserName))
                    {
                        var newFriend = new Friend
                        {
                            Id = friend.Id,
                            Name = friend.UserName
                        };
                        userEntry.Friends.Add(newFriend);
                    }
                    return userEntry;
                },
                param: param,
                commandType:CommandType.StoredProcedure,
                splitOn:"GroupId,Id"
                );
            return userDictionary.Values.FirstOrDefault();
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
