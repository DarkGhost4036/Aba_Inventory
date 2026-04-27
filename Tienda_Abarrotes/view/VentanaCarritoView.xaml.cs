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


    }
}