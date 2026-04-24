using AbaInventory.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tienda_Abarrotes.ViewModel;

namespace AbaInventory.ViewModel
{
    public class ProductoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Producto> ListaProductos { get; set; }

        public ProductoViewModel()
        {
            ListaProductos = new ObservableCollection<Producto>
            {
                new Producto { Nombre="Pepsi Lata 235 ml", Categoria="Pepsi", Stock=10, Estado="Activo", Imagen="/Images/pepsi.png" },
                new Producto { Nombre="Coca-Cola Sin Azucar Lata 235 ml", Categoria="Coca-Cola", Stock=28, Estado="Activo", Imagen="/Images/cocaSinAzucar.png",  },
                new Producto { Nombre="Sabritas 45 g", Categoria="Sabritas", Stock=0, Estado="Agotado", Imagen="/Images/sabritasOriginales.png" },
                new Producto { Nombre="Donas Bimbo 62 g", Categoria="Bimbo", Stock=1, Estado="Bajo stock", Imagen="/Images/donasBimbo.png" }
            };

            SumarCommand = new RelayCommand(Sumar);
            RestarCommand = new RelayCommand(Restar);
            AgregarCarritoCommand = new RelayCommand(Agregar);
        }

        private double total;
        public double Total
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        // Commands
        public ICommand SumarCommand { get; }
        public ICommand RestarCommand { get; }
        public ICommand AgregarCarritoCommand { get; }

        private void Sumar(object obj)
        {
            if (obj is Producto p)
            {
                p.Cantidad++; // ya notifica solo
            }
        }

        private void Restar(object obj)
        {
            if (obj is Producto p && p.Cantidad > 0)
            {
                p.Cantidad--;
            }
        }

        private void Agregar(object obj)
        {
            if (obj is Producto p && p.Cantidad > 0)
            {
                Total += p.Cantidad;
                p.Cantidad = 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}