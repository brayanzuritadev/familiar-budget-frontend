using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal interface IService<T,K> where T : class where K : class
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        List<K> GetAll();
        K Get(T entity);
        List<K> Search(string text);
    }
}
