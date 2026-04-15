using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tienda_Abarrotes.ViewModel
{
    public class RegistroViewModel : ViewModelBase
    {
        // No se necesita, pero se guarda por si se requiere en el futuro para otras operaciones
        //private readonly RepositoryBase repositoryBase;

        private ObservableCollection<UserModel> _users;
        private UserModel _user;
        private IUserRepository userRepository;

        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        public RegistroViewModel()
        {
            userRepository = new UserRepository();
            _user = new UserModel();
        }

        // --- Commands ---

        public ICommand AddCommand
        {
            get
            {
                return new ViewModelCommand(AddExecute, AddCanExecute);
            }
        }

        private void AddExecute(object user)
        {
            MessageBox.Show(
                $"UserName: {User?.UserName}\n" +
                $"Password: {User?.Password}\n" +
                $"Name: {User?.Name}\n" +
                $"LastName: {User?.LastName}\n" +
                $"Email: {User?.Email}"
            );

            if (string.IsNullOrWhiteSpace(User?.UserName) ||
                string.IsNullOrWhiteSpace(User?.Name) ||
                string.IsNullOrWhiteSpace(User?.LastName) ||
                string.IsNullOrWhiteSpace(User?.Email))
            {
                MessageBox.Show("Campos incompletos");
                return;
            }

            var existingUser = userRepository.GetByUserName(User.UserName);
            if (existingUser != null)
            {
                MessageBox.Show("Usuario duplicado");
                return;
            }

            userRepository.Add(User);

            MessageBox.Show("Usuario añadido correctamente");

            User = new UserModel(); // limpiar formulario
        }

        private bool AddCanExecute(object user)
        {
            // Deshabilita el botón si los campos están vacíos
            return true;
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new ViewModelCommand(DeleteExecute, DeleteCanExecute);
            }
        }

        private void DeleteExecute(object user)
        {
            userRepository.Delete(User); // Borra el usuario usando el Id
            // Actualizar la lista de usuarios si es necesario
            // Users = userRepository.Get();
        }

        private bool DeleteCanExecute(object user)
        {
            // Verifica que el objeto user no sea nulo y tenga un Id válido
            return true;
        }

        public ICommand EditCommand
        {
            get
            {
                return new ViewModelCommand(EditExecute, EditCanExecute);
            }
        }

        private void EditExecute(object user)
        {
            userRepository.Update(User); // Borra el usuario usando el Id
            // Actualizar la lista de usuarios si es necesario
            // Users = userRepository.Get();
        }

        private bool EditCanExecute(object user)
        {
            // Verifica que el objeto user no sea nulo y tenga un Id válido
            return true;
        }
    }
}