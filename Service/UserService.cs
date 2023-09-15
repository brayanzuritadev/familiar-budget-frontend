using Data;
using Data.DataAccess;
using Data.Dto;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IService<User, UserDetailDto>
    {
        UserDao userDao = new UserDao();
        public bool Create(User entity)
        {
            return userDao.Create(entity);
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public UserDetailDto Get(User entity)
        {
            return userDao.Get(entity);
        }

        public List<UserDetailDto> GetAll()
        {
            return userDao.GetAll();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
