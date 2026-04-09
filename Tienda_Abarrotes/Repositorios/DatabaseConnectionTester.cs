using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Abarrotes.Repositorios
{
    public class DatabaseConnectionTester : RepositoryBase
    {
        public bool TestConnection(out string message)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    message = "Conexión exitosa a la base de datos.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = $"Error al conectar:{ex.Message}";
                return false;
            }
        }

        public bool TestQuery(out string message)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT COUNT(*) FROM Usuario", connection))
                    {
                        int total = (int)command.ExecuteScalar();
                        message = $"Conexión exitosa. Registros en la tabla Usuario:{total}";
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"Error en la consulta: {ex.Message}";
                return false;
            }
        }
    }
}

