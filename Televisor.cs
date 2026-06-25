namespace TiendaElectrodomesticos
{
    public class Televisor : Producto
    {
        public int Pulgadas { get; set; }

        public string TipoPantalla { get; set; }

        public Televisor()
        {
        }

        public Televisor(
            int codigo,
            string nombre,
            decimal precio,
            int stock,
            int idSucursal,
            int pulgadas,
            string tipoPantalla)
            : base(codigo, nombre, precio, stock, idSucursal)
        {
            Pulgadas = pulgadas;
            TipoPantalla = tipoPantalla;
        }

        // Ejemplo de polimorfismo:
        // Los televisores de 55" o más tienen un recargo del 10%.
        public override decimal CalcularPrecioFinal()
        {
            if (Pulgadas >= 55)
            {
                return Precio * 1.10m;
            }

            return Precio;
        }
    }
}