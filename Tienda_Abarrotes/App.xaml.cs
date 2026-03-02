using Tienda_Abarrotes.View;   // o View si así se llama tu carpeta
using System.Windows;

namespace Tienda_Abarrotes
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Productos productos = new Productos();
            productos.Show();
        }
    }
}