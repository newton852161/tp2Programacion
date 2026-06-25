namespace TiendaElectrodomesticos
{
    public abstract class Producto
    {
        public int Codigo { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public int IdSucursal { get; set; }

        protected Producto()
        {
        }

        protected Producto(int codigo, string nombre, decimal precio, int stock, int idSucursal)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            IdSucursal = idSucursal;
        }

        // Método abstracto requerido por el TP
        public abstract decimal CalcularPrecioFinal();
    }
}