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
    public class FigureDao : ApplicationDbContext, IDao<Figure, FigureDetailDto>
    {
        public bool Create(Figure entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "INSERT INTO Figure(figurename) " +
                        "VALUES(@figurename)", entity);

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

        public bool Delete(Figure entity)
        {
            throw new NotImplementedException();
        }

        public FigureDetailDto Get(Figure entity)
        {
            throw new NotImplementedException();
        }

        public List<FigureDetailDto> GetAll()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<FigureDetailDto>(
                        "SELECT * FROM Figure"
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

        public List<FigureDetailDto> Search(string text)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<FigureDetailDto>(
                            "SELECT * FROM figure " +
                            "WHERE UPPER(figurename) like '%' || UPPER(@Text) || '%'",
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

        public bool Update(Figure entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "UPDATE figure SET figurename = @figurename " +
                        "WHERE figureid = @figureid", entity);


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
