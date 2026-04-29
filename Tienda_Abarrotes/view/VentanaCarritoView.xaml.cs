using System.Windows;
using Tienda_Abarrotes.ViewModel; 
namespace Tienda_Abarrotes.View
{
    public partial class VentanaCarritoView : Window
    {
        public VentanaCarritoView()
        {
            InitializeComponent();

            
            this.DataContext = new CarritoViewModel();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Close();
        }
    }
}