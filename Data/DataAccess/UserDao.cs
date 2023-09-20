using Data.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Data.Dto;
using static Dapper.SqlMapper;

namespace Data.DataAccess
{
    public class UserDao : ApplicationDbContext, IDao<User, UserDetailDto>
    {
        public bool Create(User entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "INSERT INTO \"user\"(name, photography, password, email, ci, roleId) " +
                        "VALUES(@Name, @Photography, @Password, @Email, @Ci, @RoleId)", entity);

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

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public UserDetailDto Get(User entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.QueryFirstOrDefault<UserDetailDto>(
                        "SELECT * FROM \"user\" WHERE email = @Email OR ci = @Ci",
                        new
                        {
                            Email = entity.Email,
                            Ci = entity.Ci
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


        public List<UserDetailDto> GetAll()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<UserDetailDto>(
                        "SELECT u.userid, u.ci, u.name, u.photography, u.email, u.roleId, r.rolename FROM \"user\" u " +
                        "INNER JOIN role r " +
                        "ON u.roleid = r.roleid"
                    ).ToList(); // Convertir los resultados a una lista

                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió una excepción al obtener datos de la base de datos: " + ex.Message);
                throw;
            }
        }

        public List<UserDetailDto> Search(string text)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var result = connection.Query<UserDetailDto>(
                        "SELECT u.userid, u.ci, u.name, u.photography, u.email, u.roleId, r.rolename FROM \"user\" u " +
                        "INNER JOIN role r " +
                        "ON u.roleid = r.roleid " +
                        "WHERE u.ci like '%' || @Text || '%' OR u.name like '%' || @Text || '%'",
                        new
                        {
                            Text = text
                        }
                    ).ToList(); // Convertir los resultados a una lista

                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió una excepción al obtener datos de la base de datos: " + ex.Message);
                throw;
            }
        }

        public bool Update(User entity)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    int rowsAffected = connection.Execute(
                        "UPDATE \"user\" SET name = @Name, photography = @Photography, " +
                        "email = @Email, ci = @Ci, roleId = @RoleId " +
                        "WHERE UserId = @UserId", entity);

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió una excepción al actualizar en la base de datos: " + ex.Message);
                return false;
            }
        }

    }
}