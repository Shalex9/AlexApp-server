﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexApp.Api.Models
{
    public class NewUser
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
