namespace TiendaElectrodomesticos
{
    public class Lavarropas : Producto
    {
        public int CargaKg { get; set; }

        public string Tipo { get; set; } // Automático o Semi

        public Lavarropas()
        {
        }

        public Lavarropas(
            int codigo,
            string nombre,
            decimal precio,
            int stock,
            int idSucursal,
            int cargaKg,
            string tipo)
            : base(codigo, nombre, precio, stock, idSucursal)
        {
            CargaKg = cargaKg;
            Tipo = tipo;
        }

        public override decimal CalcularPrecioFinal()
{
    if (Tipo != null && Tipo.ToLower() == "automatico")
        return Precio * 1.12m;

    return Precio;
}
    }
}