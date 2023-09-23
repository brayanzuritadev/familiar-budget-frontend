using Dapper;
using Data.Dto;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
                        "INSERT INTO Weapon(weaponnumber, weaponname, description) " +
                        "VALUES(@weaponnumber, @weaponname, @description)", entity);

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
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<Weapon>(
                        "SELECT * FROM Weapon"
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

        public List<Weapon> Search(string text)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<Weapon>(
                            "SELECT * FROM Weapon " +
                            "WHERE weaponnumber like '%' || @Text|| '%' OR UPPER(weaponname) like '%' || UPPER(@Text) || '%'",
                            new
                            {
                                Text = text
                            }
                        ).ToList();

                    return result;
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw ex;
            }
        }

        public bool Update(Weapon entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "UPDATE weapon SET weaponnumber = @weaponnumber, weaponname = @weaponname, description = @description " +
                        "WHERE weaponid = @weaponid", entity);


                    return rowsAffected > 0;
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error : "+ex.Message);
                throw ex;
            }
        }
    }
}
