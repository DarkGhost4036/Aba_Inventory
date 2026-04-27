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
                                        (Nombre, Estado, Stock, Categoria, Imagen)
                                        VALUES (@Nombre, @Estado, @Stock, @Categoria, @Imagen)";

                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Estado", producto.Estado);
                command.Parameters.AddWithValue("@Stock", producto.Stock);
                command.Parameters.AddWithValue("@Categoria", producto.Categoria);
                // DBNull.Value se utiiza por si la imagen viene vacía
                command.Parameters.AddWithValue("@Imagen", string.IsNullOrEmpty(producto.Imagen) ? (object)DBNull.Value : producto.Imagen);

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

                // Faltaba agregar Precio al UPDATE
                command.CommandText = @"UPDATE Producto 
                                        SET Nombre = @Nombre,
                                            Estado = @Estado,
                                            Stock = @Stock, 
                                            Categoria = @Categoria,                                                               
                                            Imagen = @Imagen                                           
                                        WHERE Id = @Id";

                command.Parameters.AddWithValue("@Id", producto.Id);
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Categoria", producto.Categoria);
                command.Parameters.AddWithValue("@Stock", producto.Stock);
                command.Parameters.AddWithValue("@Estado", producto.Estado);
                command.Parameters.AddWithValue("@Imagen", string.IsNullOrEmpty(producto.Imagen) ? (object)DBNull.Value : producto.Imagen);

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

                            // Se valida si la imagen es nula en la base de datos
                            Imagen = reader["Imagen"] != DBNull.Value ? reader["Imagen"].ToString() : string.Empty,
                        });
                    }
                }
            }
            return productos;
        }
    }
}