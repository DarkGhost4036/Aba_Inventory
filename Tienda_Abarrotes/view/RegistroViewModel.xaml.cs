using System;
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
    /// Lógica de interacción para RegistroViewModel.xaml
    /// </summary>
    public partial class RegistroViewModel : Window
    {
        public RegistroViewModel()
        {
            InitializeComponent();

            this.DataContext = new Tienda_Abarrotes.ViewModel.RegistroViewModel();
        }

        // Eventos para botones como minimizar, cerrar y cancelar

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Tienda_Abarrotes.ViewModel.RegistroViewModel vm)
            {
                vm.User.Password = txtPassword.Password;
            }
        }
    }
}