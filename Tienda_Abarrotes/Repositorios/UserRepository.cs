using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Tienda_Abarrotes.Model;

namespace Tienda_Abarrotes.Repositorios
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"INSERT INTO Usuario 
                                (UserName, Password, Name, LastName, Email)
                                VALUES (@UserName, @Password, @Name, @LastName, @Email)";

                command.Parameters.AddWithValue("@UserName", userModel.UserName);
                command.Parameters.AddWithValue("@Password", userModel.Password);
                command.Parameters.AddWithValue("@Name", userModel.Name);
                command.Parameters.AddWithValue("@LastName", userModel.LastName);
                command.Parameters.AddWithValue("@Email", userModel.Email);

                command.ExecuteNonQuery();
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText="SELECT COUNT(*) FROM Usuario WHERE UserName = @username AND  Password = @password";

                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;
                int count = (int)command.ExecuteScalar();
                validUser = (count > 0);
            }
            return validUser;
        }

        public void Delete(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Usuario WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", userModel.Id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public UserModel GetByUserName(string userName)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Usuario] WHERE UserName = @UserName";
                command.Parameters.AddWithValue("@UserName", userName);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            Id = Convert.ToInt32(reader[0]),
                            UserName = reader[1].ToString(),
                            Password = reader[2].ToString(),
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
                return user;
            }
        }

        public void Update(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand()) 
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE Usuario SET UserName = @UserName, Password = @Password, Name = @Name, LastName = @LastName, Email = @Email WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", userModel.Id);
                command.Parameters.AddWithValue("@UserName", userModel.UserName);
                command.Parameters.AddWithValue("@Password", userModel.Password);
                command.Parameters.AddWithValue("@Name", userModel.Name);
                command.Parameters.AddWithValue("@LastName", userModel.LastName);
                command.Parameters.AddWithValue("@Email", userModel.Email);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public bool TestConnection(out string message)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    message = "Conexión exitosa a la base de datos.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = $"Error al conectar con la base de datos: {ex.Message}";
                return false;
            }
        }
        public IEnumerable<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            using (var connection = GetConnection())
            using(var command = new SqlCommand("SELECT * FROM Usuario", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            Id = Convert.ToInt32(reader[0]),
                            UserName = reader[1].ToString(),
                            Password = reader[2].ToString(),
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        });
                    }
                }
            }
            return users;
        }
    }
}
