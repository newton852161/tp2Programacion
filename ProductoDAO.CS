using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace TiendaElectrodomesticos
{
    public class ProductoDAO
    {
        public void AgregarProducto(Producto producto)
        {
            using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = @"INSERT INTO Producto
                (Codigo, Nombre, Precio, Stock, IdSucursal, TipoProducto)
                VALUES
                (@Codigo, @Nombre, @Precio, @Stock, @IdSucursal, @TipoProducto)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@Codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@IdSucursal", producto.IdSucursal);
                    cmd.Parameters.AddWithValue("@TipoProducto", producto.GetType().Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> ListarProductos()
        {
            List<string> lista = new List<string>();

            using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT * FROM Producto";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        lista.Add(
                            $"{lector["Codigo"]} - " +
                            $"{lector["Nombre"]} - " +
                            $"${lector["Precio"]} - " +
                            $"Stock: {lector["Stock"]}");
                    }
                }
            }

            return lista;
        }

        public void ActualizarStock(int codigoProducto, int nuevoStock)
        {
            using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = "UPDATE Producto SET Stock=@Stock WHERE Codigo=@Codigo";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@Stock", nuevoStock);
                    cmd.Parameters.AddWithValue("@Codigo", codigoProducto);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool EliminarProducto(int codigoProducto)
        {
            try
            {
                using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    string sql = "DELETE FROM Producto WHERE Codigo=@Codigo";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Codigo", codigoProducto);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (MySqlException)
            {
                Console.WriteLine("No se puede eliminar: el producto tiene ventas registradas.");
                return false;
            }
        }

        public List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT * FROM Producto";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        string tipoProducto = lector["TipoProducto"].ToString();
                        Producto p;

                        if (tipoProducto == "Heladera")
                        {
                            p = new Heladera
                            {
                                Codigo = Convert.ToInt32(lector["Codigo"]),
                                Nombre = lector["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(lector["Precio"]),
                                Stock = Convert.ToInt32(lector["Stock"]),
                                IdSucursal = Convert.ToInt32(lector["IdSucursal"])
                            };
                        }
                        else if (tipoProducto == "Lavarropas")
                        {
                            p = new Lavarropas
                            {
                                Codigo = Convert.ToInt32(lector["Codigo"]),
                                Nombre = lector["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(lector["Precio"]),
                                Stock = Convert.ToInt32(lector["Stock"]),
                                IdSucursal = Convert.ToInt32(lector["IdSucursal"])
                            };
                        }
                        else
                        {
                            p = new Televisor
                            {
                                Codigo = Convert.ToInt32(lector["Codigo"]),
                                Nombre = lector["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(lector["Precio"]),
                                Stock = Convert.ToInt32(lector["Stock"]),
                                IdSucursal = Convert.ToInt32(lector["IdSucursal"])
                            };
                        }

                        lista.Add(p);
                    }
                }
            }

            return lista;
        }

        public bool ModificarProducto(int codigoProducto, string nuevoNombre, decimal nuevoPrecio, int nuevoStock)
        {
            try
            {
                using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    string sql = @"UPDATE Producto 
                                   SET Nombre=@Nombre, Precio=@Precio, Stock=@Stock 
                                   WHERE Codigo=@Codigo";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@Precio", nuevoPrecio);
                        cmd.Parameters.AddWithValue("@Stock", nuevoStock);
                        cmd.Parameters.AddWithValue("@Codigo", codigoProducto);

                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (MySqlException)
            {
                Console.WriteLine("Error al modificar el producto.");
                return false;
            }
        }
    }
}