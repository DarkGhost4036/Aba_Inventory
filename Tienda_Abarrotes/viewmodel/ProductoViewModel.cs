using AbaInventory.Models;
using System.Collections.ObjectModel;

namespace AbaInventory.ViewModel
{
    public class ProductoViewModel
    {
        public ObservableCollection<Producto> ListaProductos { get; set; }

        public ProductoViewModel()
        {
            ListaProductos = new ObservableCollection<Producto>
            {
                new Producto { Nombre="Pepsi Lata 235 ml", Categoria="Pepsi", Stock=10, Estado="Activo", Imagen="/Images/pepsi.png", Tiendas=5 },
                new Producto { Nombre="Coca-Cola Sin Azucar Lata 235 ml", Categoria="Coca-Cola", Stock=28, Estado="Activo", Imagen="/Images/cocaSinAzucar.png", Tiendas=3 },
                new Producto { Nombre="Sabritas 45 g", Categoria="Sabritas", Stock=0, Estado="Agotado", Imagen="/Images/sabritasOriginales.png", Tiendas=2 },
                new Producto { Nombre="Donas Bimbo 62 g", Categoria="Bimbo", Stock=1, Estado="Bajo stock", Imagen="/Images/donasBimbo.png", Tiendas=1 }
            };
        }
    }
}