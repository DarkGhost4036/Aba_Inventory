using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Tienda_Abarrotes.View
{
    public partial class BorrarProducto : Window
    {
        // Usamos ObservableCollection para que la tabla se actualice sola al borrar
        public ObservableCollection<Product> Products { get; set; }

        public BorrarProducto()
        {
            InitializeComponent();
            LoadData();

            // Asignamos los eventos de los botones de borrar
            BtnEliminarLateral.Click += BtnEliminar_Click;
            BtnEliminarTabla.Click += BtnEliminar_Click;
        }

        private void LoadData()
        {
            Products = new ObservableCollection<Product>
            {
                new Product { IsSelected = false, Name = "Pepsi Lata 235 ml", Status = "Active", Stock = "10 in stock", Category = "Pepsi", ImageUrl = "/Assets/pepsi.png" },
                new Product { IsSelected = false, Name = "Coca-Cola Lata 235 ml", Status = "Active", Stock = "28 in stock", Category = "Coca-Cola", ImageUrl = "/Assets/coke.png" },
                new Product { IsSelected = false, Name = "Sabritones chile y limon 160 g", Status = "Sold out", Stock = "0 in stock", Category = "Sabritas", ImageUrl = "/Assets/sabritones.png" },
                new Product { IsSelected = false, Name = "Bimbo Nito 62 g", Status = "Low stock", Stock = "1 in stock", Category = "Bimbo", ImageUrl = "/Assets/nito.png" }
            };

            ProductsGrid.ItemsSource = Products;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Filtramos los productos que tienen el CheckBox marcado
            var toRemove = Products.Where(p => p.IsSelected).ToList();

            if (toRemove.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona al menos un producto para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"¿Estás seguro de que deseas eliminar {toRemove.Count} producto(s)?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                foreach (var prod in toRemove)
                {
                    Products.Remove(prod);
                }
            }
        }
        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Close();
        }

        private void BtnEliminarLateral_Click(object sender, RoutedEventArgs e)
        {
            BorrarProducto ventana = new BorrarProducto();
            ventana.Show();
            this.Close();

        }

        private void btnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarProductos ventana = new AgregarProductos();
            ventana.Show();
            this.Close();
        }
        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            LoginView ventana = new LoginView();
            ventana.Show();
            this.Close(); // cierra Login
        }
        public void btnManejadorUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close(); // cierra Login
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Product
    {
        public bool IsSelected { get; set; } // Propiedad para el CheckBox
        public string Name { get; set; }
        public string Status { get; set; }
        public string Stock { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}