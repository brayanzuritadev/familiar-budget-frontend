using Dapper;
using Data.Dto;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data.DataAccess
{
    public class UnitMilitaryDao : ApplicationDbContext, IDao<UnitMilitary, UnitMilitary>
    {
        public bool Create(UnitMilitary entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "INSERT INTO militaryunit(militaryunitname, department) " +
                        "VALUES(@militaryunitname, @department)", entity);

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
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<UnitMilitary>(
                        "SELECT * FROM militaryunit"
                    ).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió una excepción al obtener datos de la base de datos: " + ex.Message);
                throw;
            }
        }

        public List<UnitMilitary> Search(string text)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<UnitMilitary>(
                            "SELECT * FROM militaryunit " +
                            "WHERE UPPER(department) like '%' || UPPER(@Text) || '%' OR UPPER(militaryunitname) like '%' || UPPER(@Text) || '%'",
                            new
                            {
                                Text = text
                            }
                        ).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw ex;
            }
        }

        public bool Update(UnitMilitary entity)
        {
            throw new NotImplementedException();
        }
    }
}
