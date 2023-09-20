using Data.DataAccess;
using Data.Entity;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class WeaponService : IService<Weapon, Weapon>
    {
        
        WeaponDao weaponDao = new WeaponDao();
        public bool Create(Weapon entity)
        {
            WeaponsValidation weaponsValidation = new WeaponsValidation();

            if (weaponsValidation.WeaponValid(entity))
                return weaponDao.Create(entity);

            return false;
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
            throw new NotImplementedException();
        }

        public List<Weapon> Search(string text)
        {
            throw new NotImplementedException();
        }

        public bool Update(Weapon entity)
        {
            throw new NotImplementedException();
        }
    }
}
