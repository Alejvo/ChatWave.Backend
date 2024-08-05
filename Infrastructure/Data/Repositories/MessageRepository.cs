using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Messages;
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
    public class MessageRepository : IMessageRepository
    {
        private readonly SqlConnectionFactory _sqlConnection;

        public MessageRepository(SqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<MessagesByGroup>> GetGroupMessages(string group)
        {
            using var connection = _sqlConnection.CreateConnection();
            var messageDictionary = new Dictionary<string, MessagesByGroup>();
            var result = await connection.QueryAsync<Group,Message,MessagesByGroup>(
                    MessageProcedure.GetGroupMessages,
                    (group,message) =>
                    {
                            if(!messageDictionary.TryGetValue(group.Id,out var messagesByGroup))
                        {
                            messagesByGroup = new MessagesByGroup
                            {
                                GroupName = group.Name,
                                //Messages = new List<Message>()
                            };
                            messageDictionary[group.Id] = messagesByGroup;
                            //messageDictionary[group.Id] = messagesByGroup;
                        };
                        messagesByGroup.Messages.Add(message);

                        return messagesByGroup;

                    },
                    param:new {GroupId=group},
                    commandType:CommandType.StoredProcedure,
                    splitOn: "Id"
                );
            return messageDictionary.Values;
        }

        public async Task<IEnumerable<MessagesBySender>> GetUserMessages(string receiver,string sender)
        {
            using var connection = _sqlConnection.CreateConnection();
            var messageDictionary = new Dictionary<string, MessagesBySender>();

            var result =  await connection.QueryAsync<User,Message,MessagesBySender>(
                    MessageProcedure.GetUserMessages,
                    (user, message) =>
                    {
                        if (!messageDictionary.TryGetValue(user.Id, out var messagesByUser))
                        {
                            messagesByUser = new MessagesBySender
                            {
                                Receiver = user.UserName
                                //Messages = new List<Message>()
                            };
                            messageDictionary[user.Id] = messagesByUser;
                            //messageDictionary[group.Id] = messagesByGroup;
                        };
                        messagesByUser.Messages.Add(message);

                        return messagesByUser;

                    },
                    param:new {ReceiverId=receiver,SenderId=sender},
                    commandType: CommandType.StoredProcedure
                );
            return messageDictionary.Values;
        }

        public async Task SendToGroup(object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            await connection.ExecuteAsync(
                 MessageProcedure.SendToGroup,
                 param,
                 commandType:CommandType.StoredProcedure
                );
        }

        public async Task SendToUser(object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            await connection.ExecuteAsync(
                 MessageProcedure.SendToUser,
                 param,
                 commandType: CommandType.StoredProcedure
                );
        }
    }
}
