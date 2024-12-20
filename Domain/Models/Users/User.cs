﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string UserName { get; set; }
        public byte[] ProfileImage { get; set; }
        public List<UserGroup> Groups { get; set; } = new List<UserGroup>();

        public List<Friend> Friends { get; set; } = new List<Friend>();
    }
}
