using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Abarrotes.Repositorios
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

         
            builder.DataSource = "localhost, 1433";


            builder.InitialCatalog = "Tienda_Abarrotes_BD";

            builder.IntegratedSecurity = false; // IMPORTANTE: Debe ser false
            builder.UserID = "usuario_equipo";
            builder.Password = "Abarrotes2026";

            // Configuraciones extra para evitar errores de red escolar
            builder.Encrypt = false;
            builder["TrustServerCertificate"] = true;

            _connectionString = builder.ConnectionString;
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
