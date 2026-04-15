using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;

namespace Tienda_Abarrotes.ViewModel
{
    public class ManejoUsuariosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserModel> Users { get; set; }

        private UserModel _selectedUser;
        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand EditCommand { get; }

        public ManejoUsuariosViewModel()
        {
            // Inicialización de datos
            Users = new ObservableCollection<UserModel>
            {
                new UserModel { Id=1, UserName="admin", Email="admin@tienda.com", LastName="Pérez" },
                new UserModel { Id=2, UserName="maria", Email="maria@tienda.com", LastName="López" }
            };

            // Implementación de comandos 
            DeleteCommand = new RelayCommand(DeleteUser);
            EditCommand = new RelayCommand(EditUser, CanEditUser);
            RefreshCommand = new RelayCommand(obj => { /* Lógica para refrescar */ });
        }

        private void DeleteUser(object parameter)
        {
            if (parameter is UserModel user)
            {
                var result = MessageBox.Show($"¿Eliminar al usuario {user.UserName}?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Users.Remove(user);
                }
            }
        }

        private void EditUser(object parameter)
        {
            MessageBox.Show("Cambios guardados correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanEditUser(object parameter)
        {
            return SelectedUser != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void CargarUsuariosDesdeBD()
        {

            Users.Clear(); // Limpiamos la lista 
            IUserRepository repo = new UserRepository();
            var UsuariosBD = repo.GetAllUsers();
            foreach(var user in UsuariosBD)
            {
                Users.Add(user);
            }
        }
                                      
                

        
    }
}