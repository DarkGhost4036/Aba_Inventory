using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;
using Tienda_Abarrotes.View;



namespace Tienda_Abarrotes.ViewModel
{



    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        public readonly IUserRepository userRepository;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class LoginViewModel : ViewModelBase   
    {
        private IUserRepository userRepository;
        public ICommand LoginCommand { get; }
        public ICommand OpenRegistroCommand { get; }
        public ICommand TestConnectionCommand { get; }



        public LoginViewModel()
        {

            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(Login);
            OpenRegistroCommand = new ViewModelCommand(OpenRegistro);
            TestConnectionCommand = new ViewModelCommand(TestConnection);

        }

        private void TestConnection(object obj)
        {
            userRepository = new UserRepository();
            string message;
            bool isConnected = userRepository.TestConnection(out message);

            MessageBox.Show(message,
                isConnected ? "Éxito" : "Error",
                MessageBoxButton.OK,
                isConnected ? MessageBoxImage.Information : MessageBoxImage.Error);
        }

        private void Login(object obj)
        {
            MessageBox.Show("Login funcionando ");

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