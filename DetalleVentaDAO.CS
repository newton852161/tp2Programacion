using MySql.Data.MySqlClient;

namespace TiendaElectrodomesticos
{
    public class DetalleVentaDAO
    {
        public void AgregarDetalle(int idVenta, DetalleVenta detalle)
        {
            using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = @"INSERT INTO detalle_venta 
                (id_venta, id_producto, cantidad, precio_unitario, subtotal)
                VALUES (@idVenta, @idProducto, @cantidad, @precio, @subtotal)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.Parameters.AddWithValue("@idProducto", detalle.Producto.Codigo);
                    cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("@precio", detalle.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@subtotal", detalle.Subtotal());

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}