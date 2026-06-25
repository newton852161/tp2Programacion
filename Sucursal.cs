using System.Collections.Generic;

namespace TiendaElectrodomesticos
{
    public class Sucursal
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        // Composición: una sucursal contiene una lista de productos
        public List<Producto> Productos { get; set; }

        public Sucursal()
        {
            Productos = new List<Producto>();
        }

        public Sucursal(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            Productos.Add(producto);
        }

        public void MostrarProductos()
        {
            foreach (Producto producto in Productos)
            {
                System.Console.WriteLine(
                    $"{producto.Codigo} - {producto.Nombre} - Stock: {producto.Stock} - Precio: ${producto.Precio}");
            }
        }
    }
}