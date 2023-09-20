using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class WeaponsValidation
    {
        WeaponService weaponService;
        public bool WeaponValid(Weapon weapon) 
        {
            weaponService = new WeaponService();

            var weaponEntity = weaponService.Get(weapon);

            if (weaponEntity?.WeaponNumber == weapon.WeaponNumber)
            {
                return false;
            }
            return true;
        }
    }
}
