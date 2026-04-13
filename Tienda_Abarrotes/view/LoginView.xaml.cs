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
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            RegistroView ventana = new RegistroView();
            ventana.Show();

            this.Close(); // cierra Login

        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();

            this.Close(); // cierra Login
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

