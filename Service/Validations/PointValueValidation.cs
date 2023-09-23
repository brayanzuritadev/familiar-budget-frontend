using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class PointValueValidation
    {
        public bool isPointValid(Point point)
        {
            if (point.PointName == "")
            {
                return false;
            }

            if (point.Value == 0)
            {
                return false;
            }
            return true;
        }
    }
}
