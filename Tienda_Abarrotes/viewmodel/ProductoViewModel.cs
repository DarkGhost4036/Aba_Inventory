using Tienda_Abarrotes.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Tienda_Abarrotes.ViewModel
{
    public class ProductoViewModel : INotifyPropertyChanged
    {
        private static ObservableCollection<Producto> _listaProductosCompartida;

        public ObservableCollection<Producto> ListaProductos
        {
            get { return _listaProductosCompartida; }
            set { _listaProductosCompartida = value; OnPropertyChanged(nameof(ListaProductos)); }
        }

        // --- PROPIEDADES PARA AGREGAR PRODUCTO ---
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        private int _stock;
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; OnPropertyChanged(nameof(Stock)); }
        }

        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; OnPropertyChanged(nameof(Codigo)); }
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

        // --- COMANDOS UNIFICADOS ---
        public ICommand GuardarProductoCommand { get; }
        public ICommand SumarCommand { get; }
        public ICommand RestarCommand { get; }
        public ICommand AgregarCarritoCommand { get; }

        public ProductoViewModel()
        {
            if (_listaProductosCompartida == null)
            {
                _listaProductosCompartida = new ObservableCollection<Producto>
                {
                    new Producto { Nombre="Pepsi Lata 235 ml", Categoria="Pepsi", Stock=10, Estado="Activo", Imagen=null },
                    new Producto { Nombre="Coca-Cola Sin Azucar Lata 235 ml", Categoria="Coca-Cola", Stock=28, Estado="Activo", Imagen=null},
                    new Producto { Nombre="Sabritas 45 g", Categoria="Sabritas", Stock=0, Estado="Agotado", Imagen=null},
                    new Producto { Nombre="Donas Bimbo 62 g", Categoria="Bimbo", Stock=1, Estado="Bajo stock", Imagen=null }
                };
            }

            GuardarProductoCommand = new RelayCommand(GuardarProducto);
            SumarCommand = new RelayCommand(Sumar);
            RestarCommand = new RelayCommand(Restar);
            AgregarCarritoCommand = new RelayCommand(Agregar);
        }

        private void GuardarProducto(object obj)
        {
            Producto nuevoProducto = new Producto
            {
                Nombre = this.Nombre,
                Stock = this.Stock,
                Categoria = this.Codigo,
                Estado = this.Stock > 0 ? "Activo" : "Agotado",
                Imagen = null,
                Tiendas = 1
            };

            ListaProductos.Add(nuevoProducto);
            MessageBox.Show("¡Producto agregado exitosamente!");

            Nombre = string.Empty;
            Stock = 0;
            Codigo = string.Empty;
        }

        private void Sumar(object obj)
        {
            if (obj is Producto p && p.Cantidad < p.Stock)
            {
                p.Cantidad++;
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
                double precioSimulado = 15.50;
                Total += (p.Cantidad * precioSimulado);
                p.Cantidad = 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}