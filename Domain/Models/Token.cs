﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Token
    {
        public string Code { get; set; }
        public string RefreshToken { get; set; }
        public DateOnly ExpiryTime { get; set; }
        public string UserId { get; set; }

    }
}
