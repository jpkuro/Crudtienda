using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudtienda
{
    internal class conexion
    {
        public static MySqlConnection Conexion()
        {
            string Servidor = "localhost";
            string bd = "tienda";
            string usuario   = "root";
            string password = "Kuro921*";

            string cadenaConexion = "Database=" + bd + "; Data Source=" + Servidor + "; User Id=" +
                usuario + "; Password=" + password+ "";

            try
            {
                MySqlConnection conexionBD = new MySqlConnection (cadenaConexion);
                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error" +  ex.Message);
                return null;
                
            }

        }
    }
}
