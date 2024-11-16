using Dapper;
using Domain.Interfaces;
using Domain.Models.Groups;
using Domain.Models.Users;
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

        public async Task AddFriend(string userId, string friendId)
        {
            using var connection = _sqlConnection.CreateConnection();
            await connection.QueryAsync(UserProcedures.AddFriend,new {userId,friendId});
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
                        userEntry.Groups = new List<UserGroup>();
                        userEntry.Friends = new List<Friend>();
                        userDictionary.Add(userEntry.Id, userEntry);
                    }
                    if (group != null && !userEntry.Groups.Any(g => g.Name == group.Name))
                    {
                        var newGroup = new UserGroup
                        {
                            Id = group.Id,
                            Name = group.Name
                        };
                        userEntry.Groups.Add(newGroup);
                    }
                    if (friend != null && !userEntry.Friends.Any(f=>f.Name ==friend.UserName))
                    {
                        var newFriend = new Friend
                        {
                            Id = friend.Id,
                            Name = friend.UserName,
                            ProfileImage = Convert.ToBase64String(friend.ProfileImage)
                        };
                        userEntry.Friends.Add(newFriend);
                    }
                    return userEntry;
                },
                commandType: CommandType.StoredProcedure,
                splitOn: "Id,Id"
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
                        userEntry.Groups = new List<UserGroup>();
                        userEntry.Friends = new List<Friend>();
                        userDictionary.Add(userEntry.Id,userEntry);
                    }
                    if (group != null && !userEntry.Groups.Any(g => g.Name == group.Name))
                    {
                        var newGroup = new UserGroup
                        {
                            Id= group.Id,
                            Name = group.Name
                        };
                        userEntry.Groups.Add(newGroup);
                    }
                    if (friend != null && !userEntry.Friends.Any(f => f.Id == friend.Id))
                    {
                        var newFriend = new Friend
                        {
                            Id = friend.Id,
                            Name = friend.UserName,
                            ProfileImage = Convert.ToBase64String(friend.ProfileImage)
                        };
                        userEntry.Friends.Add(newFriend);
                    }
                    return userEntry;
                },
                param: param,
                commandType:CommandType.StoredProcedure,
                splitOn:"Id,Id"
                );
            return userDictionary.Values.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetUsersByUsername(string username)
        {
            using var connection = _sqlConnection.CreateConnection();
            var userDictionary = new Dictionary<string, User>();
            await connection.QueryAsync<User, Group, User, User>(
                UserProcedures.GetUsersByUsername,
                (user, group, friend) =>
                {
                    if (!userDictionary.TryGetValue(user.Id, out var userEntry))
                    {
                        userEntry = user;
                        userEntry.Groups = new List<UserGroup>();
                        userEntry.Friends = new List<Friend>();
                        userDictionary.Add(userEntry.Id, userEntry);
                    }
                    if (group != null && !userEntry.Groups.Any(g => g.Name == group.Name))
                    {
                        var newGroup = new UserGroup
                        {
                            Id = group.Id,
                            Name = group.Name
                        };
                        userEntry.Groups.Add(newGroup);
                    }
                    if (friend != null && !userEntry.Friends.Any(f => f.Id == friend.Id))
                    {
                        var newFriend = new Friend
                        {
                            Id = friend.Id,
                            Name = friend.UserName,
                            ProfileImage = Convert.ToBase64String(friend.ProfileImage)
                        };
                        userEntry.Friends.Add(newFriend);
                    }
                    return userEntry;
                },
                param: new {username},
                commandType: CommandType.StoredProcedure,
                splitOn: "Id,Id"
                );
            return userDictionary.Values;
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
