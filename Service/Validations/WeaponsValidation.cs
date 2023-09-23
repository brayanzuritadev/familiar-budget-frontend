using Data.Entity;
using Service.Tools;
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
        public ResultRequest WeaponValid(Weapon weapon) 
        {
            ResultRequest resultRequest = new ResultRequest();
            weaponService = new WeaponService();

            var weaponEntity = weaponService.Get(weapon);

            if (weaponEntity?.WeaponNumber == weapon.WeaponNumber && weapon.WeaponId == 0)
            {
                resultRequest.Success = false;
                resultRequest.Messages.Add("Error! El numero de arma ya esta registrado");
            }

            if (weapon.WeaponId != weaponEntity?.WeaponId && weapon.WeaponNumber == weaponEntity?.WeaponNumber)
            {
                resultRequest.Success = false;
                resultRequest.Messages.Add("Error! El numero de arma ya esta registrado");
            }

            if (string.IsNullOrEmpty(weapon.WeaponName))
            {
                resultRequest.Success = false;
                resultRequest.Messages.Add("Error! No se envio el nombre del arma");
            }

            return resultRequest;
        }
    }
}
