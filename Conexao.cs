using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaaaaaaaaaaah
{
    public class Conexao
    {
        static private string servidor = "localhost";
        static private string database = "bd_banco";
        static private string usuario = "root";
        static private string senha = "cursoads";
        static private string strCon = $"server={servidor};" +
            $"User id={usuario}; password = {senha}; database={database}";

        MySqlConnection cn;

        public bool Conectar()
        {
            bool resultado = false;

            try
            {
                cn = new MySqlConnection(strCon);
                cn.Open();
                resultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);

            }
            return resultado;
        }

        public void Desconectar()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }

        public bool Executa(string sql)
        {
            bool result;
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, cn);
                comando.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                result = false;
            }

            return result;
        }

        public DataTable Retorna(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand mc = new MySqlCommand(sql, cn);
                MySqlDataAdapter da = new MySqlDataAdapter(); // Esse objeto nem tem parametro pois chama o select
                da.SelectCommand = mc;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            return dt;

        }
    }
}
