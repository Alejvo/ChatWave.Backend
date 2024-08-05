using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Messages
{
    public class MessagesBySender
    {
        public string Receiver { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
