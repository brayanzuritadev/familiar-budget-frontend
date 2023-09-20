using Dapper;
using Data.Dto;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class WeaponDao : ApplicationDbContext, IDao<Weapon, Weapon>
    {
        public bool Create(Weapon entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "INSERT INTO Weapon(weaponnumber, description) " +
                        "VALUES(@weaponnumber, @description)", entity);

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió una excepción al insertar en la base de datos: " + ex.Message);
                return false;
                throw;
            }
        }

        public bool Delete(Weapon entity)
        {
            throw new NotImplementedException();
        }

        public Weapon Get(Weapon entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.QueryFirstOrDefault<Weapon>(
                        "SELECT * FROM Weapon WHERE WeaponNumber = @WeaponNumber",
                        new
                        {
                            WeaponNumber = entity.WeaponNumber
                        }
                    );

                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió una excepción al obtener datos de la base de datos: " + ex.Message);
                throw;
            }
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
