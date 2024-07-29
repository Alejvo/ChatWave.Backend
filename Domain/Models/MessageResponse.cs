using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MessageResponse
    {
        public string Sender {  get; set; }
        public string Receiver {  get; set; }
        public string Text { get; set; }
    }
}
