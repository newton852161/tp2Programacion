using System;

namespace TiendaElectrodomesticos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================");
            Console.WriteLine(" TIENDA DE ELECTRODOMÉSTICOS");
            Console.WriteLine("=================================");

            int opcion;

            do
            {
                Console.WriteLine("\nMenú Principal");
                Console.WriteLine("1 - Agregar producto");
                Console.WriteLine("2 - Listar productos");
                Console.WriteLine("3 - Registrar venta");
                Console.WriteLine("4 - Salir");
                Console.WriteLine("5 - Eliminar producto");
                Console.WriteLine("6 - Modificar producto");
                Console.Write("Seleccione una opción: ");
                

                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                {
    Console.Write("Tipo de producto (1=Televisor, 2=Heladera, 3=Lavarropas): ");
    int tipo = int.Parse(Console.ReadLine());

    Console.Write("Código: ");
    int codigo = int.Parse(Console.ReadLine());
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine();
    Console.Write("Precio: ");
    decimal precio = decimal.Parse(Console.ReadLine());
   Console.Write("Stock: ");
    int stock = int.Parse(Console.ReadLine());

    Console.Write("ID de sucursal (1 o 2): ");
    int idSucursal = int.Parse(Console.ReadLine());

    ProductoDAO dao = new ProductoDAO();

    if (tipo == 1)
    {
        Console.Write("Pulgadas: ");
        int pulgadas = int.Parse(Console.ReadLine());
        Console.Write("Tipo de pantalla: ");
        string tipoPantalla = Console.ReadLine();
        dao.AgregarProducto(new Televisor(codigo, nombre, precio, stock, idSucursal, pulgadas, tipoPantalla));
    }
    else if (tipo == 2)
    {
        Console.Write("Capacidad en litros: ");
        int litros = int.Parse(Console.ReadLine());
        Console.Write("Tipo (No Frost / Freezer): ");
        string tipoHel = Console.ReadLine();
        dao.AgregarProducto(new Heladera(codigo, nombre, precio, stock, idSucursal, litros, tipoHel));
    }
    else if (tipo == 3)
    {
        Console.Write("Carga en kg: ");
        int kg = int.Parse(Console.ReadLine());
        Console.Write("Tipo (Automatico / Semi): ");
        string tipoLav = Console.ReadLine();
        dao.AgregarProducto(new Lavarropas(codigo, nombre, precio, stock, idSucursal, kg, tipoLav));
    }

    Console.WriteLine("Producto agregado correctamente.");
    break;
}
                  case 2:
{
    Console.WriteLine("\nLISTA DE PRODUCTOS");
    Console.WriteLine("====================");

    ProductoDAO dao = new ProductoDAO();
    var productos = dao.ObtenerProductos();

    foreach (var p in productos)
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Código: {p.Codigo}");
        Console.WriteLine($"Nombre: {p.Nombre}");
        Console.WriteLine($"Precio: {p.Precio}");
        Console.WriteLine($"Stock: {p.Stock}");
        Console.WriteLine("--------------------------------");
    }

    Console.WriteLine("Fin de la lista.");
    break;
}
                case 3:
{
    // 1. Crear la venta con fecha actual y sucursal
    Venta venta = new Venta();
    venta.IdSucursal = 1; // o pedirlo por consola si querés

    // 2. Mostrar productos disponibles para que el usuario elija
    ProductoDAO productoDAO = new ProductoDAO();
    var productos = productoDAO.ObtenerProductos();

    Console.WriteLine("\n--- PRODUCTOS DISPONIBLES ---");
    foreach (var p in productos)
        Console.WriteLine($"Código: {p.Codigo} | {p.Nombre} | Precio: {p.Precio} | Stock: {p.Stock}");

    // 3. Bucle para agregar productos al carrito
    string seguir = "s";
    while (seguir.ToLower() == "s")
    {
        Console.Write("\nIngrese el código del producto: ");
        int codigo = int.Parse(Console.ReadLine());

        // Buscar el producto en la lista
        Producto productoElegido = productos.Find(p => p.Codigo == codigo);

        if (productoElegido == null)
        {
            Console.WriteLine("Producto no encontrado.");
        }
        else
        {
            Console.Write("Cantidad: ");
            int cantidad = int.Parse(Console.ReadLine());

            DetalleVenta detalle = new DetalleVenta(productoElegido, cantidad);
            venta.AgregarDetalle(detalle);
            Console.WriteLine("Producto agregado a la venta.");
        }

        Console.Write("¿Agregar otro producto? (s/n): ");
        seguir = Console.ReadLine();
    }

    // 4. Mostrar resumen y guardar en la BD
    venta.MostrarResumen();

    VentaDAO ventaDAO = new VentaDAO();
    ventaDAO.RegistrarVenta(venta);

    Console.WriteLine("Venta registrada correctamente.");
    break;
}
            case 5:
        {
    ProductoDAO dao = new ProductoDAO();
    var productos = dao.ObtenerProductos();

    Console.WriteLine("\n--- PRODUCTOS DISPONIBLES ---");
    foreach (var p in productos)
        Console.WriteLine($"Código: {p.Codigo} | {p.Nombre}");

    Console.Write("\nIngrese el código del producto a eliminar: ");
    int codigo = int.Parse(Console.ReadLine());

    bool eliminado = dao.EliminarProducto(codigo);
    if (eliminado)
    Console.WriteLine("Producto eliminado correctamente.");
    break;
}
case 6:
{
    ProductoDAO dao = new ProductoDAO();
    var productos = dao.ObtenerProductos();

    Console.WriteLine("\n--- PRODUCTOS DISPONIBLES ---");
    foreach (var p in productos)
        Console.WriteLine($"Código: {p.Codigo} | {p.Nombre} | Precio: {p.Precio} | Stock: {p.Stock}");

    Console.Write("\nIngrese el código del producto a modificar: ");
    int codigo = int.Parse(Console.ReadLine());

    Console.Write("Nuevo nombre: ");
    string nuevoNombre = Console.ReadLine();

    Console.Write("Nuevo precio: ");
    decimal nuevoPrecio = decimal.Parse(Console.ReadLine());

    Console.Write("Nuevo stock: ");
    int nuevoStock = int.Parse(Console.ReadLine());

    bool modificado = dao.ModificarProducto(codigo, nuevoNombre, nuevoPrecio, nuevoStock);
    if (modificado)
        Console.WriteLine("Producto actualizado.");

    break;
}
                    case 4:
                        Console.WriteLine("Hasta luego.");
                        break;
                    

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
                

            } while (opcion != 4);
        }
    }
}