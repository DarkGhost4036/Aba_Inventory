using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tienda_Abarrotes.ViewModel;
using Tienda_Abarrotes.Model;

namespace Tienda_Abarrotes.View
{
    public partial class BorrarProducto : Window
    {
        public BorrarProducto()
        {
            InitializeComponent();

            // Se conecta esta ventana a la memoria central compartida
            this.DataContext = new ProductoViewModel();

            // Se asignan los eventos de los botones de borrar
            BtnEliminarLateral.Click += BtnEliminar_Click;
            BtnEliminarTabla.Click += BtnEliminar_Click;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Extraer el ViewModel para acceder a la lista compartida
            var viewModel = (ProductoViewModel)this.DataContext;

            // Se filtran los productos que tienen el CheckBox marcado
            var toRemove = viewModel.ListaProductos.Where(p => p.IsSelected).ToList();

            if (toRemove.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona al menos un producto para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"¿Estás seguro de que deseas eliminar {toRemove.Count} producto(s)?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Conexión a la Base de Datos para eliminar los productos seleccionados
                // Se le pasa la lista completa al ViewModel para que los borre de la base de datos real.
                viewModel.EliminarProductosSeleccionados(toRemove);

                MessageBox.Show("Productos eliminados permanentemente de la base de datos.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Close();
        }

        private void BtnEliminarLateral_Click(object sender, RoutedEventArgs e)
        {
            BorrarProducto ventana = new BorrarProducto();
            ventana.Show();
            this.Close();
        }

        private void btnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarProductos ventana = new AgregarProductos();
            ventana.Show();
            this.Close();
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            LoginView ventana = new LoginView();
            ventana.Show();
            this.Close();
        }

        public void btnManejadorUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}