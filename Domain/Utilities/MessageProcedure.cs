using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilities
{
    public static class MessageProcedure
    {
        public const string SendToGroup = "SendMessageToGroup";
        public const string SendToUser = "SendMessageToUser";
        public const string GetGroupMessages = "GetGroupMessages";
        public const string GetUserMessages = "GetUserMessages";
    }
}
