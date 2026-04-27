using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tienda_Abarrotes.ViewModel;

namespace Tienda_Abarrotes.View
{
    /// <summary>
    /// Lógica de interacción para AgregarProductos.xaml
    /// </summary>
    public partial class AgregarProductos : Window
    {
        public AgregarProductos()
        {
            InitializeComponent();
            // Línea para conectar la Vista con la Lógica MVVM
            // Conectamos con ProductoViewModel para manejar la lógica de productos
            this.DataContext = new ProductoViewModel();
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

        public void btnManejadorUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close();
        }

        public void ManejadorUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close();
        }

        private void btnConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            // Lógica de configuración pendiente
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}