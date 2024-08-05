using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Messages
{
    public class Message
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string SenderName { get; set; }  
    }
}
