using System.ComponentModel;

namespace Tienda_Abarrotes.Model
{
    public class Producto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }
        public int Tiendas { get; set; }
        public bool IsSelected { get; set; }

        public string TiendasTexto => Tiendas == 1 ? "1 store" : $"{Tiendas} stores";

        // Propiedad para la cantidad en el carrito, con notificación de cambio

        private int cantidad;
        public int Cantidad
        {
            get => cantidad;
            set
            {
                cantidad = value;
                OnPropertyChanged(nameof(Cantidad));
            }
        }

        // --- CONSTRUCTORES ---
        public Producto() { }

        public Producto(string nombre, string categoria, int stock, string estado, int tiendas)
        {
            Nombre = nombre;
            Estado = estado;
            Stock = stock;
            Categoria = categoria;
            Tiendas = tiendas;
        }

        // --- EVENTO DE NOTIFICACIÓN ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}