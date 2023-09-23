using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Point
    {
        public int PointId { get; set; }
        public string PointName { get; set; }
        public int Value { get; set; }
        public int FigureId { get; set; }
    }
}
