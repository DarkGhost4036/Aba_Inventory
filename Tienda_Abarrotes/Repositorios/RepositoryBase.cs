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
            // Usamos el constructor de conexiones para evitar cualquier error de texto o formato
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"LENOVO_LOQ\VSGESTION"; // Tu servidor
            builder.InitialCatalog = "Tienda_Abarrotes_BD"; // Tu base de datos
            builder.IntegratedSecurity = true; // Credenciales de Windows

            // Aquí están los dos seguros anti-errores SSL
            builder.Encrypt = false;
            builder["TrustServerCertificate"] = true;

            // C# arma la cadena de texto perfecta automáticamente
            _connectionString = builder.ConnectionString;
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
