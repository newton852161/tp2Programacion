namespace TiendaElectrodomesticos
{
    public class Heladera : Producto
    {
        public int CapacidadLitros { get; set; }

        public string Tipo { get; set; } // No Frost o Freezer

        public Heladera()
        {
        }

        public Heladera(
            int codigo,
            string nombre,
            decimal precio,
            int stock,
            int idSucursal,
            int capacidadLitros,
            string tipo)
            : base(codigo, nombre, precio, stock, idSucursal)
        {
            CapacidadLitros = capacidadLitros;
            Tipo = tipo;
        }

        public override decimal CalcularPrecioFinal()
{
    if (Tipo != null && Tipo.ToLower() == "no frost")
        return Precio * 1.15m;

    return Precio;
}
    }
}