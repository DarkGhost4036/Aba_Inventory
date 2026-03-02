namespace AbaInventory.Models
{
    public class Producto
    {
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
        public int Tiendas { get; set; }
        public string Imagen { get; set; }
        public string TiendasTexto => Tiendas == 1 ? "1 store" : $"{Tiendas} stores";
        public Producto()
        {
        }
        public Producto(string nombre, string categoria, int stock, string estado, int tiendas)
        {
            Nombre = nombre;
            Estado = estado;
            Stock = stock;
            Categoria = categoria;
            Tiendas = tiendas;
        }
    }
}