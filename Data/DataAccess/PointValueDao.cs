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
    public class PointValueDao : ApplicationDbContext, IDao<Point, PointDetailDto>
    {
        public bool Create(Point entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "INSERT INTO Point(pointName, value, figureid) " +
                        "VALUES(@PointName, @Value, @figureid)", entity);

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
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<PointDetailDto>(
                        "SELECT * FROM point"
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

        public List<PointDetailDto> Search(string text)
        {
            throw new NotImplementedException();
        }

        public bool Update(Point entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "UPDATE point SET pointname = @pointname, value = @value, figureid = @figureid " +
                        "WHERE pointid = @pointid", entity);


                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
                throw ex;
            }
        }
    }
}
