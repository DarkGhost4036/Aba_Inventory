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

   
        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is LoginViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}