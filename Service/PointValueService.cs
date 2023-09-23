using Data.DataAccess;
using Data.Dto;
using Data.Entity;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PointValueService : IService<Point, PointDetailDto>
    {
        PointValueDao pointValueDao = new PointValueDao();
        public bool Create(Point entity)
        {
            var pointValidation = new PointValueValidation();
            if (pointValidation.isPointValid(entity))
            {
                return pointValueDao.Create(entity);
            }
            return false;
        }

        public bool Delete(Point entity)
        {
            throw new NotImplementedException();
        }

        public PointDetailDto Get(Point entity)
        {
            throw new NotImplementedException();
        }

        public List<PointDetailDto> GetAll()
        {
            return pointValueDao.GetAll();
        }

        public List<PointDetailDto> Search(string text)
        {
            throw new NotImplementedException();
        }

        public bool Update(Point entity)
        {
            if (entity.PointName == "")
            {
                return false;
            }

            if (entity.Value == 0)
            {
                return false;
            }

            return pointValueDao.Update(entity);
        }
    }
}
