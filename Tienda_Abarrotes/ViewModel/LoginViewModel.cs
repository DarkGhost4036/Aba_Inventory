using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;
using Tienda_Abarrotes.ViewModel;

namespace Tienda_Abarrotes.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        
        //Campos
        private string _username; //Almacena el usuario 
        //Almacena la contraseña de forma segura
        private string _password;
        //Guarda el mensaje de error en caso de que el login falle
        private string _errorMessage;
        //Controla si la ventana de login está visible
        public bool _isViewVisible = true;
        // me permite acceder a los métodos de la interfaz
        //IUserRepository
        private IUserRepository _userRepository;


        //Propiedades
        
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value; OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value ;OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage= value ;OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get {  return _isViewVisible; }
            set
            {
                _isViewVisible= value ;OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        //Comandos
        //LogginCommand se ejecuta cuando el ususario hace clic aquí
        public ICommand LoginCommand { get; }
        //Muestra u oculta  la contraseña 
        public ICommand ShowPasswordCommand {  get; }
        //Constructor
        public LoginViewModel()
        {
            _userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            TestConnectionCommand = new ViewModelCommand(ExecuteTestConnectionCommand);
            VerificarConexion();// verifica la conexion a la base de datos
        }
        public ICommand TestConnectionCommand { get; }
        private void VerificarConexion()
        {
            string mensaje;
            bool conexionExitosa = _userRepository.TestConnection(out mensaje);
            ErrorMessage = mensaje;
        }
        private void ExecuteTestConnectionCommand(object obj)
        {
            String mensaje;
            bool conexionExitosa = _userRepository.TestConnection(out mensaje);
            //Muestra el resultado en la interfaz
            ErrorMessage = mensaje;
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length <3 || Password==null || Password.Length < 3){
                validData = false;

            }
            else
            {
                validData = true;
            }
            return validData;
        }
        public void ExecuteLoginCommand(object obj) {
            var isValidUser = _userRepository.AuthenticateUser(
            new System.Net.NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal= new GenericPrincipal(new GenericIdentity(Username),null);
                IsViewVisible = false;//Oculta una vez usado, la ventana de login
                ErrorMessage = string.Empty;
            }
            else
            {
                ErrorMessage = "*Invalid username or password";
            }
        }
    }
}
