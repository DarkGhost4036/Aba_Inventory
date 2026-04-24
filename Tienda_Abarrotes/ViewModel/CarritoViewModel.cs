using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_Abarrotes.Model;

namespace Tienda_Abarrotes.ViewModel
{
    public class CarritoViewModel
    {
        public ObservableCollection<CarritoItem> ItemsCarrito { get; set; }
            public CarritoViewModel()
        {
            ItemsCarrito= new ObservableCollection<CarritoItem>();
        }

        //mostrar el total en la interfaz
        public double TotalAPagar => ItemsCarrito.Sum(item => item.Subtotal);

        //metodo para agregar productos
        public void AgregarAlCarrito(string nombreProducto, double precio, int cantidad)
        {
            var itemExistente = ItemsCarrito.FirstOrDefault(i => i.NombreProducto == nombreProducto);
            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad; // Si el producto ya existe, aumentamos la cantidad
            }
            else
            {
                ItemsCarrito.Add(new CarritoItem
                {
                    NombreProducto = nombreProducto,
                    Precio = precio,
                    Cantidad = cantidad
                });
                OnPropertyChanged(nameof(TotalAPagar));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nombreProducto) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombreProducto));
    
    }
}
