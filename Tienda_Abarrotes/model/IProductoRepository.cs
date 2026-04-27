using System.Collections.Generic;

namespace Tienda_Abarrotes.Model
{
    public interface IProductoRepository
    {
        void Add(Producto producto);
        void Update(Producto producto);
        void Delete(Producto producto);
        IEnumerable<Producto> GetAllProductos();
    }
}