using Data.DataAccess;
using Data.Dto;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FigureService : IService<Figure, FigureDetailDto>
    {
        FigureDao figureDao = new FigureDao();
        public bool Create(Figure entity)
        {
            return figureDao.Create(entity);
        }

        public bool Delete(Figure entity)
        {
            throw new NotImplementedException();
        }

        public List<FigureDetailDto> Get(Figure entity)
        {
            throw new NotImplementedException();
        }

        public List<FigureDetailDto> GetAll()
        {
           return figureDao.GetAll();
        }

        public List<FigureDetailDto> Search(string text)
        {
            return figureDao.Search(text);
        }

        public bool Update(Figure entity)
        {
            return figureDao.Update(entity);
        }

        FigureDetailDto IService<Figure, FigureDetailDto>.Get(Figure entity)
        {
            throw new NotImplementedException();
        }
    }
}
