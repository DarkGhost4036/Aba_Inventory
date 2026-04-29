using System.Windows;
using System.Windows.Controls;
using Tienda_Abarrotes.ViewModel; // Asegúrate de tener este using arriba

namespace Tienda_Abarrotes.View
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is LoginViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}