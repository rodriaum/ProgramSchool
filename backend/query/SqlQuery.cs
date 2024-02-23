using MySql.Data.MySqlClient;

namespace SchoolQuery.backend.query
{
    internal class SqlQuery
    {

        internal void CreateDatabasesIfNotExists(MySqlConnection connection)
        {
            try
            {
                foreach (string databaseName in Enum.GetNames(typeof(SchoolClass)))
                {
  
                    string query = $"CREATE DATABASE IF NOT EXISTS {databaseName};";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"Banco de dados {databaseName} criado com sucesso (ou já existente).");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar os bancos de dados: " + ex.Message);
            }
        }

        internal void AddStudent(MySqlConnection connection, SchoolClass schoolClass, int number, string name, double average)
        {
            try
            {
                string query = "INSERT INTO " + schoolClass.ToString().ToLower() + " (number, name, average) VALUES (@number, @name, @average)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@number", number);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@average", average);

                    command.ExecuteNonQuery();

                    Console.WriteLine("Aluno adicionado com sucesso.");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar aluno: " + ex.Message);
            }
        }
    }
}
