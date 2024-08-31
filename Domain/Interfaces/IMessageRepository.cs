using Domain.Models;
using Domain.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task SendToUser(object param);
        Task SendToGroup(object param);
        Task<IEnumerable<MessagesByGroup>> GetGroupMessages(string receiver,string group);
        Task<IEnumerable<MessagesBySender>> GetUserMessages(string receiver,string sender);
    }
}
