using System.Windows;

namespace Tienda_Abarrotes.View
{
    public partial class ManejoUsuarioViewModel : Window
    {
        public ManejoUsuarioViewModel()
        {
            InitializeComponent();
            this.DataContext = new Tienda_Abarrotes.ViewModel.ManejoUsuariosViewModel();
        }
    }
}