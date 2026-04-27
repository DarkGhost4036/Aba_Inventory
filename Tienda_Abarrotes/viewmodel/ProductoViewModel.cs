using Tienda_Abarrotes.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace Tienda_Abarrotes.ViewModel
{
    public class ProductoViewModel : INotifyPropertyChanged
    {
        // --- MEMORIA COMPARTIDA ---
        // Vital para que la ventana de Agregar, Mostrar y Eliminar vean los mismos datos
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

        // --- PROPIEDADES DEL CARRITO ---
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

        // --- CONSTRUCTOR ---
        public ProductoViewModel()
        {
            // Inicialización segura de la memoria global
            if (_listaProductosCompartida == null)
            {
                _listaProductosCompartida = new ObservableCollection<Producto>
                {
                    new Producto { Nombre="Pepsi Lata 235 ml", Categoria="Pepsi", Stock=10, Estado="Activo", Imagen="/Images/pepsi.png", Tiendas=5 },
                    new Producto { Nombre="Coca-Cola Sin Azucar Lata 235 ml", Categoria="Coca-Cola", Stock=28, Estado="Activo", Imagen="/Images/cocaSinAzucar.png", Tiendas=3 },
                    new Producto { Nombre="Sabritas 45 g", Categoria="Sabritas", Stock=0, Estado="Agotado", Imagen="/Images/sabritasOriginales.png", Tiendas=2 },
                    new Producto { Nombre="Donas Bimbo 62 g", Categoria="Bimbo", Stock=1, Estado="Bajo stock", Imagen="/Images/donasBimbo.png", Tiendas=1 }
                };
            }

            // Enlace de los comandos con sus respectivos métodos
            GuardarProductoCommand = new RelayCommand(GuardarProducto);
            SumarCommand = new RelayCommand(Sumar);
            RestarCommand = new RelayCommand(Restar);
            AgregarCarritoCommand = new RelayCommand(Agregar);
        }

        // --- LÓGICA DE MÉTODOS ---

        // Método para agregar al inventario (Tu código)
        private void GuardarProducto(object obj)
        {
            Producto nuevoProducto = new Producto
            {
                Nombre = this.Nombre,
                Stock = this.Stock,
                Categoria = this.Codigo,
                Estado = this.Stock > 0 ? "Activo" : "Agotado",
                Imagen = "https://via.placeholder.com/50",
                Tiendas = 1
            };

            ListaProductos.Add(nuevoProducto);
            MessageBox.Show("¡Producto agregado exitosamente!");

            Nombre = string.Empty;
            Stock = 0;
            Codigo = string.Empty;
        }

        // Métodos para el punto de venta/carrito
        private void Sumar(object obj)
        {
            if (obj is Producto p)
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
                Total += p.Cantidad;
                p.Cantidad = 0;
            }
        }

        // --- NOTIFICACIÓN A LA VISTA ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}