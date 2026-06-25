using MySql.Data.MySqlClient;

namespace TiendaElectrodomesticos
{
    public static class ConexionBD
    {
        private static string cadenaConexion =
            "Server=localhost;Database=tienda_electrodomesticos;Uid=root;Pwd=;";

        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }
    }
}