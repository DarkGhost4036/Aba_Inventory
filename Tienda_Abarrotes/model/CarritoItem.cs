using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Abarrotes.Model
{
    public class CarritoItem
    {
        public string NombreProducto { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal => Precio * Cantidad;//propiedad para calcular datos
    }
}
