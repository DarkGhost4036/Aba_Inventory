using Microsoft.Win32;
using System;
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
        // --- MEMORIA COMPARTIDA ---
        // Esto es vital para que la ventana de Agregar, Mostrar y Eliminar vean los mismos datos
        private static ObservableCollection<Producto> _listaProductos;      
        public ObservableCollection<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; OnPropertyChanged(nameof(ListaProductos)); }
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
            // Inicialización segura de la memoria global
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
            ListaProductos = new ObservableCollection<Producto>(productosBD);
        }
        private void GuardarProducto(object obj)
        {
            string rutaFinalBD = "/Imagen/default.png"; // Imagen por defecto en caso de que no se seleccione una imagen
            // Cuando el usuario haya seleccionado una imagen, se copia a la carpeta "Images" del proyecto
            if (!string.IsNullOrEmpty(RutaImagen) && File.Exists(RutaImagen))
            {
                // Se obtiene la ruta de la carpeta "Images"
                string directorioProyecto = AppDomain.CurrentDomain.BaseDirectory;
                string carpetaImages = Path.Combine(directorioProyecto, "Images");

                // Si la carpeta "Images" no existe, entonces se crea
                if (!Directory.Exists(carpetaImages))
                {
                    Directory.CreateDirectory(carpetaImages);
                }

                // Nombre único para evitar que imágenes con el mismo nombre choquen
                string extension = Path.GetExtension(RutaImagen);
                string nombreUnico = DateTime.Now.Ticks.ToString() + extension;
                string rutaDestino = Path.Combine(carpetaImages, nombreUnico);

                // Se copia el archivo
                File.Copy(RutaImagen, rutaDestino);

                // Esta es la ruta que se guardará en la base de datos
                rutaFinalBD = "/Images/" + nombreUnico;
            }

            Producto nuevoProducto = new Producto
            {
                Nombre = this.Nombre,
                Stock = this.Stock,
                Categoria = this.Codigo,
                Estado = this.Stock > 0 ? "Activo" : "Agotado",
                Imagen = rutaFinalBD,
                Tiendas = 1
            };

            _productoRepository.Add(nuevoProducto);
            MessageBox.Show("¡Producto agregado exitosamente!");

            CargarProductosBD(); // Refrescar la lista para mostrar el nuevo producto

            Nombre = string.Empty;
            Stock = 0;
            Codigo = string.Empty;
            RutaImagen = null;
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