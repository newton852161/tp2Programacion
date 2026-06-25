namespace TiendaElectrodomesticos
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public DetalleVenta()
        {
        }

        public DetalleVenta(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = producto.CalcularPrecioFinal();
        }

        public decimal Subtotal()
        {
            return PrecioUnitario * Cantidad;
        }
    }
}