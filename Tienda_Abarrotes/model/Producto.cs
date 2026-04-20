using System.ComponentModel;

namespace AbaInventory.Models
{
    public class Producto : INotifyPropertyChanged
    {
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
        public int Tiendas { get; set; }
        public string Imagen { get; set; }

        public string TiendasTexto => Tiendas == 1 ? "1 store" : $"{Tiendas} stores";

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

        public Producto() { }

        public Producto(string nombre, string categoria, int stock, string estado, int tiendas)
        {
            Nombre = nombre;
            Estado = estado;
            Stock = stock;
            Categoria = categoria;
            Tiendas = tiendas;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}