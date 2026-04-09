using AbaInventory.ViewModel;
using System.Windows;
using Tienda_Abarrotes.ViewModel;

namespace Tienda_Abarrotes.View
{
    public partial class Productos : Window
    {
        public Productos()
        {
            InitializeComponent();
            DataContext = new ProductoViewModel();
        }
    }
}