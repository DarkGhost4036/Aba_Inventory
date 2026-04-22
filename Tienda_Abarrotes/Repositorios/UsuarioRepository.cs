using System;
using System.Data.SqlClient;
using Tienda_Abarrotes.Repositorios;

public class UsuarioRepository : RepositoryBase
{
    public bool ValidarUsuario(string userName, string passWord)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                // Ajusta los nombres de tabla y columnas a los que tengas en SQL
                command.CommandText = "SELECT COUNT(*) FROM Usuario WHERE NombreUsuario = @username AND Password = @password";
                command.Parameters.AddWithValue("@username", userName);
                command.Parameters.AddWithValue("@password", passWord);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }
}