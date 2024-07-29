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
    public class MessageRepository : IMessageRepository
    {
        private readonly SqlConnectionFactory _sqlConnection;

        public MessageRepository(SqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<MessageResponse>> GetGroupMessages(string group)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryAsync<MessageResponse>(
                    MessageProcedure.GetGroupMessages,
                    new {GroupId=group},
                    commandType:CommandType.StoredProcedure
                );
        }

        public async Task<IEnumerable<MessageResponse>> GetUserMessages(string user)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryAsync<MessageResponse>(
                    MessageProcedure.GetUserMessages,
                    new {UserId=user},
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
