using System;
using System.Collections.Generic;
using System.Linq;

namespace TiendaElectrodomesticos
{
    public class Venta
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int IdSucursal { get; set; }

        public List<DetalleVenta> Detalles { get; set; }

        public Venta()
        {
            Fecha = DateTime.Now;
            Detalles = new List<DetalleVenta>();
        }

        public void AgregarDetalle(DetalleVenta detalle)
        {
            Detalles.Add(detalle);
        }

        public decimal CalcularTotal()
        {
            return Detalles.Sum(d => d.Subtotal());
        }

        public void MostrarResumen()
        {
            Console.WriteLine("===== RESUMEN DE VENTA =====");
            Console.WriteLine($"Fecha: {Fecha}");

            foreach (var detalle in Detalles)
            {
                Console.WriteLine(
                    $"{detalle.Producto.Nombre} x {detalle.Cantidad} = ${detalle.Subtotal()}");
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine($"TOTAL: ${CalcularTotal()}");
        }
    }
}