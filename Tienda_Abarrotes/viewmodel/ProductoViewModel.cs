using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;
using System.Linq;


namespace Tienda_Abarrotes.ViewModel
{
    public class ProductoViewModel : INotifyPropertyChanged
    {
        private readonly IProductoRepository _productoRepository;
        // --- MEMORIA COMPARTIDA ---
        // Esto es vital para que la ventana de Agregar, Mostrar y Eliminar vean los mismos datos
        private static ObservableCollection<Producto> _listaProductos;      
        public ObservableCollection<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; OnPropertyChanged(nameof(ListaProductos)); }
        }



        // <-- para el carrito 
        public static ObservableCollection<Producto> _carritoProductos;
        public ObservableCollection<Producto> CarritoProductos
        {
            get { return _carritoProductos; }
            set { _carritoProductos = value; OnPropertyChanged(nameof(CarritoProductos)); }
        }




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

        private decimal _precio;
        public decimal Precio
        {
            get { return _precio; }
            set
            {
                _precio = value;
                OnPropertyChanged(nameof(Precio));
            }
        }

        private string _rutaImagen;
        public string RutaImagen
        {
            get { return _rutaImagen; }
            set { _rutaImagen = value; OnPropertyChanged(nameof(RutaImagen)); }
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
        public ICommand SeleccionarImagenCommand { get; }
        public ICommand GuardarProductoCommand { get; }
        public ICommand SumarCommand { get; }
        public ICommand RestarCommand { get; }
        public ICommand AgregarCarritoCommand { get; }

        // --- CONSTRUCTOR ---
        public ProductoViewModel()
        {


            // <-- para el carrito 
            if (_listaProductos == null) _listaProductos = new ObservableCollection<Producto>();
            if (_carritoProductos == null) _carritoProductos = new ObservableCollection<Producto>();


            _productoRepository = new ProductoRepository();
            CargarProductosBD();         

            // Enlace de los comandos con sus respectivos métodos
            GuardarProductoCommand = new RelayCommand(GuardarProducto);
            SeleccionarImagenCommand = new RelayCommand(SeleccionarImagen);
            SumarCommand = new RelayCommand(Sumar);
            RestarCommand = new RelayCommand(Restar);
            AgregarCarritoCommand = new RelayCommand(Agregar);
        }

        // --- LÓGICA DE MÉTODOS ---

        // Método para agregar al inventario
        private void SeleccionarImagen(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Seleccionar imagen del producto";
            dialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png"; // Solo permitir formatos comunes de imagen
            // Esto mustra una imgaen de vista previa, después de que el usuario haya cargado una imagen
            if (dialog.ShowDialog() == true)
            {
                RutaImagen = dialog.FileName;
            }
        }
         private void CargarProductosBD()
        {
            var productosBD = _productoRepository.GetAllProductos();
            ListaProductos.Clear();
            foreach (var p in productosBD)
            {
                ListaProductos.Add(p);
            }

        }
        private void GuardarProducto(object obj)
        {
            byte[] imagenBytes = null;

            // Si el usuario seleccionó una imagen y el archivo existe, lo convertimos a bytes directo
            if (!string.IsNullOrEmpty(RutaImagen) && File.Exists(RutaImagen))
            {
                imagenBytes = File.ReadAllBytes(RutaImagen);
            }

            Producto nuevoProducto = new Producto
            {
                Nombre = this.Nombre,
                Stock = this.Stock,
                Categoria = this.Codigo,
                Estado = this.Stock > 0 ? "Activo" : "Agotado",
                Precio = this.Precio,
                Imagen = imagenBytes, // Se van los bytes directos a la BD
                
            };

            _productoRepository.Add(nuevoProducto);
            MessageBox.Show("¡Producto agregado exitosamente!");

            CargarProductosBD();


            Nombre = string.Empty;
            Stock = 0;
            Codigo = string.Empty;
            Precio = 0;
            RutaImagen = null;
        }

        public void EliminarProductosSeleccionados(List<Producto> productosAEliminar)
        {
            foreach (var producto in productosAEliminar)
            {
                // Consulta a la BD y ejecuta el DELETE usando el Id del producto
                _productoRepository.Delete(producto);
            }

            // Recarga la lista desde la base de datos para que la tabla en pantalla se actualice
            CargarProductosBD();
        }

        // Métodos para el punto de venta/carrito
        private void Sumar(object obj)
        {
            if (obj is Producto p&& p.Cantidad < p.Stock)
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
                double precioProducto = 15.50;

                // CAMBIO: En lugar de llamar al otro ViewModel, agrégalo a la lista de ESTE ViewModel
                var itemExistente = _carritoProductos.FirstOrDefault(x => x.Nombre == p.Nombre);
                if (itemExistente != null)
                {
                    itemExistente.Cantidad += p.Cantidad;
                }
                else
                {
                    _carritoProductos.Add(new Producto
                    {
                        Nombre = p.Nombre,
                        Cantidad = p.Cantidad,
                        Stock = p.Stock,
                        Imagen = p.Imagen
                    });
                }

                Total += (p.Cantidad);

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