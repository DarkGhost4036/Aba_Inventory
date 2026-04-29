using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;

namespace Tienda_Abarrotes.ViewModel
{
    public class ManejoUsuariosViewModel : INotifyPropertyChanged
    {
        // Usamos tu interfaz exactamente como la definiste
        private readonly IUserRepository _userRepository;

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

        // Comandos CRUD
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RefreshCommand { get; }

        public ManejoUsuariosViewModel()
        {
            _userRepository = new UserRepository();
            Users = new ObservableCollection<UserModel>();

            AddCommand = new RelayCommand(AddUser);
            DeleteCommand = new RelayCommand(DeleteUser);
            EditCommand = new RelayCommand(EditUser, CanEditUser);
            RefreshCommand = new RelayCommand(RefreshUsers);

            CargarUsuariosDesdeBD();
        }

    

        // 1. CONSULTAR
        private void CargarUsuariosDesdeBD()
        {
            try
            {
                Users.Clear();
                var usuariosBD = _userRepository.GetAllUsers(); 

                foreach (var user in usuariosBD)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshUsers(object parameter)
        {
            CargarUsuariosDesdeBD();
        }

        // 2. GUARDAR
        private void AddUser(object parameter)
        {
            View.RegistroView ventanaRegistro = new View.RegistroView();
            ventanaRegistro.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }

        }

        // 3. ACTUALIZAR
        private void EditUser(object parameter)
        {
            if (SelectedUser == null) return;

            // 1. VALIDAR DUPLICADOS PRIMERO
            var userDuplicate = Users.Any(u => u.UserName.Equals(SelectedUser.UserName, StringComparison.OrdinalIgnoreCase) && u.Id != SelectedUser.Id);
            if (userDuplicate)
            {
                MessageBox.Show("El nombre de usuario ya está siendo usado por otra persona.", "Usuario Duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Detiene el proceso
            }

            var emailDuplicate = Users.Any(u => u.Email.Equals(SelectedUser.Email, StringComparison.OrdinalIgnoreCase) && u.Id != SelectedUser.Id);
            if (emailDuplicate)
            {
                MessageBox.Show("Este correo electrónico ya está registrado en otra cuenta.", "Correo Duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Detiene el proceso
            }

            // 2. SI TODO ESTÁ BIEN, ACTUALIZAR
            try
            {
                _userRepository.Update(SelectedUser);
                MessageBox.Show("Cambios guardados correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarUsuariosDesdeBD();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanEditUser(object parameter)
        {
            return SelectedUser != null;
        }

        // 4. ELIMINAR
        private void DeleteUser(object parameter)
        {
            // Tu método Delete pide el modelo completo, así que se lo pasamos
            if (parameter is UserModel user)
            {
                var result = MessageBox.Show($"¿Estás seguro de eliminar al usuario {user.UserName}?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _userRepository.Delete(user); // Actualizado a tu método Delete
                        Users.Remove(user);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        // --- INotifyPropertyChanged ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}