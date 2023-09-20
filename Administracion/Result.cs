using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion
{
    public class Result
    {
        public List<string> messages = new List<string>();
        public bool success = true;

        public Result() {
        
            messages.Add("OK");
            messages.Add("OK");
            messages.Add("OK");
        }
    }
}
