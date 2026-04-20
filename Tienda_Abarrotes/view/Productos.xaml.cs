using AbaInventory.ViewModel;
using System.Windows;
using System.Windows.Controls;
using Tienda_Abarrotes.ViewModel;

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

        private void BtnCarrito_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}