using System.Data.SqlClient;
using System.Configuration;

namespace Testes
{
    public class Conexao
    {
        SqlConnection con = new SqlConnection();
        private SqlConnection conexao()
        {
            con.ConnectionString = "Server=ADSTDFDES08; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;";
                return con;
        }
        public SqlConnection conectar()
        {
            con.ConnectionString = "Server=ADSTDFDES08; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;";
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        public void desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}