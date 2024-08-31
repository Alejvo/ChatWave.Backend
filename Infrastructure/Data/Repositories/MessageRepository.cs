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

        public async Task<IEnumerable<MessagesByGroup>> GetGroupMessages(string receiver,string group)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryAsync<MessagesByGroup>(
                    MessageProcedure.GetGroupMessages,
                    param:new {GroupId=group},
                    commandType:CommandType.StoredProcedure
                );
            
        }

        public async Task<IEnumerable<MessagesBySender>> GetUserMessages(string receiver,string sender)
        {
            using var connection = _sqlConnection.CreateConnection();

            return await connection.QueryAsync<MessagesBySender>(
                    MessageProcedure.GetUserMessages,
                    param:new {ReceiverId=receiver,SenderId=sender},
                    commandType: CommandType.StoredProcedure
                );
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
