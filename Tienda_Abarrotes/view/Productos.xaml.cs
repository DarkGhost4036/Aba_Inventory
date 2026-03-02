using System.Windows;
using AbaInventory.ViewModels;

namespace Tienda_Abarrotes.View
{
    public partial class Productos : Window
    {
        public Productos()
        {
            InitializeComponent();
            DataContext = new ProductoViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
