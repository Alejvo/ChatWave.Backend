﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Messages
{
    public class MessagesByGroup
    {
        public string SenderId { get; set; }
        public string GroupId { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
    }
}
