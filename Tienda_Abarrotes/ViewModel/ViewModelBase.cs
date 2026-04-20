using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;
using Tienda_Abarrotes.View;

namespace Tienda_Abarrotes.ViewModel
{
    
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private readonly IUserRepository userRepository;

        protected IUserRepository UserRepository => userRepository;

        public ViewModelBase()
        {
            userRepository = new UserRepository();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   
    public class LoginViewModel : ViewModelBase
    {
        
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand OpenRegistroCommand { get; }
        public ICommand TestConnectionCommand { get; }

        public LoginViewModel()
        {
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
            

            Productos ventana = new Productos();
            ventana.Show();
            Application.Current.Windows[0]?.Close();

        }


        private void OpenRegistro(object obj)
        {
            RegistroView ventana = new RegistroView();
            ventana.Show();
            

            Application.Current.Windows[0]?.Close();
        }
    }
}