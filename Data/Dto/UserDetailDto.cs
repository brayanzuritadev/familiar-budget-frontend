﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class UserDetailDto
    {
        public int UserId { get; set; }
        public int Ci { get; set; }
        public string Name { get; set; }
        public string Photography { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName {  get; set; }
    }
}
