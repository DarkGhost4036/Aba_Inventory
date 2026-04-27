using Microsoft.Win32; // <-- MUY IMPORTANTE PARA QUE FUNCIONE OpenFileDialog
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Tienda_Abarrotes.Model;
using Tienda_Abarrotes.Repositorios;

namespace Tienda_Abarrotes.ViewModel
{
    public class ProductoViewModel : INotifyPropertyChanged
    {
        private readonly IProductoRepository _productoRepository;

        private static ObservableCollection<Producto> _listaProductos;
        public ObservableCollection<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; OnPropertyChanged(nameof(ListaProductos)); }
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

        private string _rutaImagen;
        public string RutaImagen
        {
            get { return _rutaImagen; }
            set { _rutaImagen = value; OnPropertyChanged(nameof(RutaImagen)); }
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

        public ICommand SeleccionarImagenCommand { get; }
        public ICommand GuardarProductoCommand { get; }
        public ICommand SumarCommand { get; }
        public ICommand RestarCommand { get; }
        public ICommand AgregarCarritoCommand { get; }

        public ProductoViewModel()
        {
            _productoRepository = new ProductoRepository();
            CargarProductosBD();

            GuardarProductoCommand = new RelayCommand(GuardarProducto);
            SeleccionarImagenCommand = new RelayCommand(SeleccionarImagen);
            SumarCommand = new RelayCommand(Sumar);
            RestarCommand = new RelayCommand(Restar);
            AgregarCarritoCommand = new RelayCommand(Agregar);
        }

        private void SeleccionarImagen(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Seleccionar imagen del producto";
            dialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";

            if (dialog.ShowDialog() == true)
            {
                RutaImagen = dialog.FileName; // Guardamos la ruta absoluta real
            }
        }

        private void CargarProductosBD()
        {
            var productosBD = _productoRepository.GetAllProductos();
            ListaProductos = new ObservableCollection<Producto>(productosBD);
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
                Imagen = imagenBytes, // Se van los bytes directos a la BD
                Tiendas = 1
            };

            _productoRepository.Add(nuevoProducto);
            MessageBox.Show("¡Producto agregado exitosamente!");

            CargarProductosBD();

           
            Nombre = string.Empty;
            Stock = 0;
            Codigo = string.Empty;
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