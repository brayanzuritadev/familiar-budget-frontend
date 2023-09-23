using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class FigureDetailDto
    {
        public int FigureId { get; set; }
        public string FigureName { get; set;}
        //public List<Point> Point { get; set; }
    }
}
