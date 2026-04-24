using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using Tienda_Abarrotes.customcontrols;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;
using Tienda_Abarrotes.View;


namespace Tienda_Abarrotes.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserRepository UserRepository;

        private string username;
        public string Username
        {
            get => username;
            set { username = value; OnPropertyChanged(nameof(Username)); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set { errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        // Comandos
        public ICommand LoginCommand { get; }
        public ICommand OpenRegistroCommand { get; }
        public ICommand TestConnectionCommand { get; }

        // Constructor
        public LoginViewModel()
        {
            UserRepository = new UserRepository(); // Iniciamos la conexión

            LoginCommand = new ViewModelCommand(Login);
            OpenRegistroCommand = new ViewModelCommand(OpenRegistro);
            TestConnectionCommand = new ViewModelCommand(TestConnection);
        }

        private void TestConnection(object obj)
        {
            string message;
            bool isConnected = UserRepository.TestConnection(out message);

            MessageBox.Show(
                message,
                isConnected ? "Éxito" : "Error",
                MessageBoxButton.OK,
                isConnected ? MessageBoxImage.Information : MessageBoxImage.Error
            );
        }

        private void Login(object obj)
        {
            
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Por favor, ingresa usuario y contraseña.";
                return;
            }

            try
            {
              
                bool isValidUser = UserRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));

                if (isValidUser)
                {
                    Productos ventana = new Productos();
                    ventana.Show();
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    ErrorMessage = "Usuario o contraseña incorrectos en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error de conexión: " + ex.Message;
            }
        }

        private void OpenRegistro(object obj)
        {
            RegistroView ventana = new RegistroView();
            ventana.Show();

            // Cerramos la ventana actual
            Application.Current.Windows[0]?.Close();
        }
    }
}
