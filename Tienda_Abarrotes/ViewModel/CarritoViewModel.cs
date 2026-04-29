using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_Abarrotes.Model;
using System.Windows.Input;
using System.Windows;

namespace Tienda_Abarrotes.ViewModel
{
    public class CarritoViewModel : INotifyPropertyChanged
    {
        // 1. MEMORIA ESTÁTICA: Los datos viven aquí y no se borran al cerrar la ventana
        public static ObservableCollection<Producto> _itemsCarritoEstaticos = new ObservableCollection<Producto>();

       
        public ObservableCollection<Producto> ItemsCarrito
        {
            get => _itemsCarritoEstaticos;
        }

        public decimal TotalAPagar => _itemsCarritoEstaticos.Sum(item => item.Subtotal);

        public ICommand FinalizarVentaCommand { get; }

        public CarritoViewModel()
        {
            // Inicializamos el comando de cobrar
            // Nota: Usa 'RelayCommand' o 'ViewModelCommand' según como se llame tu clase de comandos
            FinalizarVentaCommand = new RelayCommand(FinalizarVenta);

            // Escuchamos la lista: si se agrega o quita algo, avisamos a la vista que el Total cambió
            _itemsCarritoEstaticos.CollectionChanged += (s, e) => {
                OnPropertyChanged(nameof(TotalAPagar));
            };
        }
      
        private void FinalizarVenta(object obj)
        {
            if (_itemsCarritoEstaticos.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"¡Venta realizada con éxito!\nTotal cobrado: {TotalAPagar:C}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpiamos el carrito para la siguiente venta
            _itemsCarritoEstaticos.Clear();
            OnPropertyChanged(nameof(TotalAPagar));
        }

        // --- NOTIFICACIÓN A LA VISTA ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}