using Domain.Models;
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
        Task<IEnumerable<MessageResponse>> GetGroupMessages(string group);
        Task<IEnumerable<MessageResponse>> GetUserMessages(string user);
    }
}
