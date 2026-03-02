using AbaInventory.Models;
using System.Collections.ObjectModel;

namespace AbaInventory.ViewModels
{
    public class ProductoViewModel
    {
        public ObservableCollection<Producto> ListaProductos { get; set; }

        public ProductoViewModel()
        {
            ListaProductos = new ObservableCollection<Producto>
            {
                new Producto { Nombre="Pepsi Lata 235 ml", Categoria="Pepsi", Stock=10, Estado="Activo", Imagen="/Images/pepsi.jpg", Tiendas=5 },
                new Producto { Nombre="Coca-Cola Sin Azucar Lata 235 ml", Categoria="Coca-Cola", Stock=28, Estado="Activo", Imagen="/Images/cocaSinAzucar.jpg", Tiendas=3 },
                new Producto { Nombre="Sabritas 45 g", Categoria="Sabritas", Stock=0, Estado="Agotado", Imagen="/Images/sabritasOriginales.jpg", Tiendas=2 },
                new Producto { Nombre="Donas Bimbo 62 g", Categoria="Bimbo", Stock=1, Estado="Bajo stock", Imagen="/Images/donasBimbo.jpg", Tiendas=1 }
            };
        }
    }
}