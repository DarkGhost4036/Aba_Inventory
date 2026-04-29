using Tienda_Abarrotes.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Tienda_Abarrotes.View
{
    public partial class Productos : Window
    {
        public Productos()
        {
            InitializeComponent();
            DataContext = new ProductoViewModel();
        }
        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            LoginView ventana = new LoginView();
            ventana.Show();
            this.Close(); // cierra Login
        }
        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnCategorias_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnFinanzas_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btnConfiguración_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarProductos ventana = new AgregarProductos();
            ventana.Show();
            this.Close();
        }
        public void btnManejadorUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close(); // cierra Login
        }


        

        public void btnManejadorUsuarios(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close(); // cierra Login
        }
        private void BtnEliminarLateral_Click(object sender, RoutedEventArgs e)
        {
            BorrarProducto ventana = new BorrarProducto();
            ventana.Show();
            this.Close();

        }

        private void VerCarrito_Click(object sender, RoutedEventArgs e)
        {
            // Creamos la ventana de visualización del carrito
            var ventanaCarrito = new VentanaCarritoView();

            // Le pasamos el contexto de datos actual (que ya tiene los productos agregados)
            // 'this.DataContext' es tu ProductosViewModel
            ventanaCarrito.DataContext = this.DataContext;

            ventanaCarrito.ShowDialog();
        } 
      
        public BitmapImage ConvertirBytesAImagen(byte[] datosBinarios)
        {
            if (datosBinarios == null || datosBinarios.Length == 0) return null;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(datosBinarios))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze(); 
            return image;
        }

        private void dataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginView ventana = new LoginView();
            ventana.Show();
            this.Close();
        }
    }
}