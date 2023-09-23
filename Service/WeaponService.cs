using Data.DataAccess;
using Data.Entity;
using Service.Tools;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service
{
    public class WeaponService : IService<Weapon, Weapon>
    {
        
        WeaponDao weaponDao = new WeaponDao();
        public ResultRequest Create(Weapon entity)
        {
            ResultRequest result = new ResultRequest();
            WeaponsValidation weaponsValidation = new WeaponsValidation();
            var weaponValid = weaponsValidation.WeaponValid(entity);

            if (weaponValid.Success)
            {
                result.Success = weaponDao.Create(entity);
                return result;
            }
            
            return weaponValid;
        }

        public bool Delete(Weapon entity)
        {
            throw new NotImplementedException();
        }

        public Weapon Get(Weapon entity)
        {
            return weaponDao.Get(entity);
        }

        public List<Weapon> GetAll()
        {
            return weaponDao.GetAll();
        }

        public List<Weapon> Search(string text)
        {
            return weaponDao.Search(text);
        }

        public bool Update(Weapon entity)
        {
            var weaponValidation = new WeaponsValidation();

            if (weaponValidation.WeaponValid(entity).Success)
            {
                return weaponDao.Update(entity);
            }
            else
            {
                MessageBox.Show("El numero de arma ya existe");
                return false;
            }
        }

        bool IService<Weapon, Weapon>.Create(Weapon entity)
        {
            throw new NotImplementedException();
        }
    }
}
