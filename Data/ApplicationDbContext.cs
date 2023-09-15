using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class ApplicationDbContext
    {
        private readonly string connectionString;

        public ApplicationDbContext()
        {
            //connectionString = "Host=dpg-cjue9bnhdsdc73a8kgqg-a.oregon-postgres.render.com;Port=5432;Username=brayan;Password=H2NzcR1WQRYtoHUZHdxb2iqsmZLe36iv;Database=poligono;";
            connectionString = "Host=localhost;Port=5432;Username=postgres;Password=dev;Database=postgres;";

        }

        protected NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
