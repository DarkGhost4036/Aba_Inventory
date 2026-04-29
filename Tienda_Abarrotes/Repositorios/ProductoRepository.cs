using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Tienda_Abarrotes.Model;

namespace Tienda_Abarrotes.Repositorios
{
    internal class ProductoRepository : RepositoryBase, IProductoRepository
    {
        public void Add(Producto producto)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"INSERT INTO Producto 
                                        (Nombre, Estado, Stock, Categoria, Imagen, Precio)
                                        VALUES (@Nombre, @Estado, @Stock, @Categoria, @Imagen, @Precio)";

                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Estado", producto.Estado);
                command.Parameters.AddWithValue("@Stock", producto.Stock);
                command.Parameters.AddWithValue("@Categoria", producto.Categoria);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.Add("@Imagen", System.Data.SqlDbType.VarBinary).Value =
                    (producto.Imagen != null && producto.Imagen.Length > 0) ? (object)producto.Imagen : DBNull.Value;
                

                command.ExecuteNonQuery();
            }
        }

        public void Delete(Producto producto)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Producto WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", producto.Id);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Producto producto)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"UPDATE Producto 
                                        SET Nombre = @Nombre,
                                            Estado = @Estado,
                                            Stock = @Stock, 
                                            Categoria = @Categoria,                                                               
                                            Imagen = @Imagen
                                            Precio = @Precio
                                        WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", producto.Id);
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Categoria", producto.Categoria);
                command.Parameters.AddWithValue("@Stock", producto.Stock);
                command.Parameters.AddWithValue("@Estado", producto.Estado);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.Add("@Imagen", System.Data.SqlDbType.VarBinary).Value =
                    (producto.Imagen != null && producto.Imagen.Length > 0) ? (object)producto.Imagen : DBNull.Value;
                

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Producto> GetAllProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT * FROM Producto", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Categoria = reader["Categoria"].ToString(),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            Estado = reader["Estado"].ToString(),
                            Precio = reader["Precio"] != DBNull.Value ? Convert.ToDecimal(reader["Precio"]) : 0m,

                            Imagen = reader["Imagen"] != DBNull.Value ? (byte[])reader["Imagen"] : null
                        });
                    }
                }
            }
            return productos;
        }
    }
}