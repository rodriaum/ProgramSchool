using MySql.Data.MySqlClient;

namespace SchoolQuery.backend
{
    internal class Connection
    {
        public MySqlConnection MySqlConnection { get; private set; }

        public Connection(string database, string user, string password)
        {
            Console.WriteLine("Tentando conexão...");

            MySqlConnection = new MySqlConnection($"Server=127.0.0.1;database={database};uid={user};pwd={password};");

            try
            {
                MySqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível estabelecer a conexão: " + e.Message);
            }

            if (MySqlConnection.State == System.Data.ConnectionState.Open)
                Console.WriteLine("Conexão estabelecida com sucesso.");
            else
                Console.WriteLine("Não foi possível abrir a conexão.");
        }
    }
}