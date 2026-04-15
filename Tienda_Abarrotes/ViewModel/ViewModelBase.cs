using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Tienda_Abarrotes.View;

namespace Tienda_Abarrotes.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class LoginViewModel : ViewModelBase   
    {
        public ICommand LoginCommand { get; }
        public ICommand OpenRegistroCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(Login);
            OpenRegistroCommand = new ViewModelCommand(OpenRegistro);
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