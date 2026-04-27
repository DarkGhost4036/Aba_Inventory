using Tienda_Abarrotes.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Tienda_Abarrotes.View
{
    public partial class RegistroView : Window
    {
        public RegistroView()
        {
            InitializeComponent();
        }

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
            LoginView ventana = new LoginView();
            ventana.Show();
            this.Close();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistroViewModel vm)
            {
                vm.User.Password = ((PasswordBox)sender).Password; // 🔥 CORRECTO
            }
        }
    }
}