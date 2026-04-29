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
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }     
        public bool IsSelected { get; set; }

       

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

        // --- Contructores ---
        public Producto() { }

        public Producto(string nombre, string categoria, int stock, string estado, decimal precio)
        {
            Nombre = nombre;
            Estado = estado;
            Stock = stock;
            Categoria = categoria;
            Precio = precio;          
      
        }

        // --- Evento de notificacion ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}