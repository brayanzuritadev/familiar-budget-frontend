﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tools
{
    public class ResultRequest
    {
        public List<string> Messages { get; set; }
        public bool Success { get; set; }
        public ResultRequest()
        {
            Messages = new List<string>();
            Success = true;
        }
    }
}
