using AbaInventory.ViewModel;
using System.Windows;
using System.Windows.Controls;
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
        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            LoginView ventana = new LoginView();
            ventana.Show();
            this.Close(); // cierra Login
        }
        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnCategorias_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnFinanzas_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btnConfiguración_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarProductos ventana = new AgregarProductos();
            ventana.Show();
            this.Close();
        }

        private void btn_mas_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            StackPanel panel = btn.Parent as StackPanel;
            TextBox txt = panel.Children[1] as TextBox;

            if (int.TryParse(txt.Text, out int cantidad))
            {
                cantidad++;
                txt.Text = cantidad.ToString();
            }
            else
            {
                txt.Text = "0";
            }
        }

        private void btn_menos_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            StackPanel panel = btn.Parent as StackPanel;
            TextBox txt = panel.Children[1] as TextBox;

            if (int.TryParse(txt.Text, out int cantidad))
            {
                if (cantidad > 0)
                    cantidad--;

                txt.Text = cantidad.ToString();
            }
            else
            {
                txt.Text = "0";
            }
        }

        public void btnManejadorUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ManejoUsuarioViewModel ventana = new ManejoUsuarioViewModel();
            ventana.Show();
            this.Close(); // cierra Login
        }
        private void BtnEliminarLateral_Click(object sender, RoutedEventArgs e)
        {
            BorrarProducto ventana = new BorrarProducto();
            ventana.Show();
            this.Close();

        }
    }
}