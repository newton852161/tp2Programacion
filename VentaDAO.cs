using MySql.Data.MySqlClient;

namespace TiendaElectrodomesticos
{
    public class VentaDAO
    {
        public void RegistrarVenta(Venta venta)
        {
            using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                MySqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    string sqlVenta =
                        @"INSERT INTO Venta(Fecha, IdSucursal)
                          VALUES(@Fecha, @IdSucursal)";

                    MySqlCommand cmdVenta =
                        new MySqlCommand(sqlVenta, conexion, transaccion);

                    cmdVenta.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmdVenta.Parameters.AddWithValue("@IdSucursal", venta.IdSucursal);

                    cmdVenta.ExecuteNonQuery();

                    long idVenta = cmdVenta.LastInsertedId;

                    foreach (DetalleVenta detalle in venta.Detalles)
                    {
                        string sqlDetalle =
                            @"INSERT INTO DetalleVenta
                            (IdVenta, CodigoProducto, Cantidad, PrecioUnitario)
                            VALUES
                            (@IdVenta, @CodigoProducto, @Cantidad, @PrecioUnitario)";

                        MySqlCommand cmdDetalle =
                            new MySqlCommand(sqlDetalle, conexion, transaccion);

                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                        cmdDetalle.Parameters.AddWithValue("@CodigoProducto", detalle.Producto.Codigo);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);

                        cmdDetalle.ExecuteNonQuery();

                        string sqlStock =
                            @"UPDATE Producto
                              SET Stock = Stock - @Cantidad
                              WHERE Codigo = @Codigo";

                        MySqlCommand cmdStock =
                            new MySqlCommand(sqlStock, conexion, transaccion);

                        cmdStock.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmdStock.Parameters.AddWithValue("@Codigo", detalle.Producto.Codigo);

                        cmdStock.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }
        public int CrearVenta()
{
    using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
    {
        string sql = "INSERT INTO venta (fecha) VALUES (NOW()); SELECT LAST_INSERT_ID();";

        using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
        {
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
    }
}