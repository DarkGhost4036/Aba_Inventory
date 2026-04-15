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
            this.Close(); // cierra Login
        }


        private void btnGuardarProducto_Click(object sender, RoutedEventArgs e)
        {

        }
        public void ManejadorUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close(); // cierra Login
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
