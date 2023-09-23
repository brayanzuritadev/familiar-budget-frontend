using Data.DataAccess;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UnitMilitaryService : IService<UnitMilitary, UnitMilitary>
    {
        UnitMilitaryDao unit = new UnitMilitaryDao();
        public bool Create(UnitMilitary entity)
        {
            return unit.Create(entity);
        }

        public bool Delete(UnitMilitary entity)
        {
            throw new NotImplementedException();
        }

        public UnitMilitary Get(UnitMilitary entity)
        {
            throw new NotImplementedException();
        }

        public List<UnitMilitary> GetAll()
        {
            return unit.GetAll();
        }

        public List<UnitMilitary> Search(string text)
        {
            return unit.Search(text);
        }

        public bool Update(UnitMilitary entity)
        {
            throw new NotImplementedException();
        }
    }
}
