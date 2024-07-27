using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Token
    {
        public string Id { get; set; }
        public string JwtToken { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string UserId { get; set; }

    }
}
