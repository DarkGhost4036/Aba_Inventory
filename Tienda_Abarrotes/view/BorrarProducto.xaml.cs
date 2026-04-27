using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.ViewModel;
using System.IO;

namespace Tienda_Abarrotes.View
{
    public partial class BorrarProducto : Window
    {
        // Usamos Producto (la del modelo) para que coincida con el ViewModel
        public ObservableCollection<Producto> Products { get; set; }

        public BorrarProducto()
        {
            InitializeComponent();
            LoadDataFromDb(); // Cambiamos el nombre para que sea claro

            BtnEliminarLateral.Click += BtnEliminar_Click;
            BtnEliminarTabla.Click += BtnEliminar_Click;
        }

        private void LoadDataFromDb()
        {
            //  Inicializamos la lista en blanco
            Products = new ObservableCollection<Producto>();

            // Tu cadena de conexión a SQL Server
            string connectionString = "Server=localhost;Database=Tienda_Abarrotes_BD;Trusted_Connection=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //La consulta para traer todos los productos
                    string query = "SELECT Id, Nombre, Estado, Stock, Categoria, Imagen FROM Producto";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    //  Recorremos lo que nos regresó SQL y lo metemos a tu lista
                    while (reader.Read())
                    {
                        Products.Add(new Producto
                        {
                            Id = (int)reader["Id"],
                            IsSelected = false,
                            Nombre = reader["Nombre"].ToString(),
                            Estado = reader["Estado"].ToString(),
                            Stock = (int)reader["Stock"],
                            Categoria = reader["Categoria"].ToString(),
                            // Validamos por si algún producto no tiene foto guardada
                            Imagen = reader["Imagen"] != DBNull.Value ? (byte[])reader["Imagen"] : null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error al cargar la base de datos: " + ex.Message, "Error de Conexión");
            }
   
            ProductsGrid.ItemsSource = Products;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //  Sacamos los que tienen la palomita
            var toRemove = Products.Where(p => p.IsSelected).ToList();

            if (toRemove.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona al menos un producto.", "Aviso");
                return;
            }

            var result = MessageBox.Show($"¿Eliminar {toRemove.Count} producto(s)?", "Confirmar", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                //  Conexión directa a SQL para borrarlos de verdad
                string connectionString = "Server=localhost;Database=Tienda_Abarrotes_BD;Trusted_Connection=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Abrimos conexión una sola vez

                    foreach (var prod in toRemove)
                    {
                       
                        string query = "DELETE FROM Producto WHERE Id = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", prod.Id);
                        cmd.ExecuteNonQuery(); // ¡Aquí se muere en SQL!

                      
                        Products.Remove(prod);
                    }
                }

                MessageBox.Show("Productos eliminados permanentemente de la base de datos.", "Éxito");
            }
        }

        // --- Eventos de Navegación ---
        private void btnProductos_Click(object sender, RoutedEventArgs e) { new Productos().Show(); this.Close(); }
        private void btnAgregarProducto_Click(object sender, RoutedEventArgs e) { new AgregarProductos().Show(); this.Close(); }
        private void btnInicio_Click(object sender, RoutedEventArgs e) { new LoginView().Show(); this.Close(); }

        // --- Métodos que faltaban del Menú Lateral ---

        private void BtnEliminarLateral_Click(object sender, RoutedEventArgs e)
        {
            // Este es el botón rojo de la izquierda. 
            // Como ya estás en esta ventana, solo la recarga.
            BorrarProducto ventana = new BorrarProducto();
            ventana.Show();
            this.Close();
        }

        public void btnManejadorUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close();
        }

        private void ProductsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}