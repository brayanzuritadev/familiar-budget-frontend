﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Ci { get; set; }
        public string Name { get; set; }
        public string Photography {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
