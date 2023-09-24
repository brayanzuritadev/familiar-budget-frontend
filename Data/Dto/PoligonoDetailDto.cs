using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class PoligonoDetailDto
    {
        public int UserId { get; set; }
        public string Ci { get; set; }
        public string Name { get; set; }
        public string Photography { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public int WeaponId { get; set; }
        public string WeaponNumber { get; set; }
        public string WeaponName { get; set; }
        public string Description { get; set; }

        public int LineId { get; set; }
        public string Line {  get; set; }
    }
}
