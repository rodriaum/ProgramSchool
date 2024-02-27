using MySql.Data.MySqlClient;

namespace SchoolQuery.backend.query
{
    internal class SqlQuery
    {
        internal void CreateTablesIfNotExists(MySqlConnection connection)
        {
            try
            {
                string useDatabaseQuery = $"USE {connection.Database};";

                using (MySqlCommand useDatabaseCommand = new MySqlCommand(useDatabaseQuery, connection))
                {
                    useDatabaseCommand.ExecuteNonQuery();
                }

                foreach (string className in Enum.GetNames(typeof(SchoolClass)))
                {
                    string tableExistsQuery = $"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{connection.Database}' AND table_name = '{className.ToLower()}';";

                    using (MySqlCommand tableExistsCommand = new MySqlCommand(tableExistsQuery, connection))
                    {

                        if (Convert.ToInt32(tableExistsCommand.ExecuteScalar()) == 0)
                        {
                            string createTableQuery = $"CREATE TABLE {className.ToLower()} (number INT PRIMARY KEY, name VARCHAR(255), average DOUBLE);";
                            using (MySqlCommand createTableCommand = new MySqlCommand(createTableQuery, connection))
                            {
                                createTableCommand.ExecuteNonQuery();
                                Console.WriteLine($"Tabela {className.ToLower()} criada com sucesso.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Usando tabela {className.ToLower()} já existente.");
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erro ao criar as tabelas: " + e.Message);
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

                    if (command.ExecuteNonQuery() > 0)
                        Console.WriteLine($"Aluno nome '{name}' adicionado com sucesso.");
                }
            }

            catch (MySqlException e)
            {
                Console.WriteLine("Erro ao adicionar aluno: " + e.Message);
            }
        }

        internal void RemoveStudent(MySqlConnection connection, SchoolClass schoolClass, int studentNumber)
        {
            try
            {
                string query = $"DELETE FROM {schoolClass.ToString().ToLower()} WHERE number = @number";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@number", studentNumber);

                    if (command.ExecuteNonQuery() > 0)
                        Console.WriteLine($"Aluno número '{studentNumber}' removido com sucesso.");
                    else
                        Console.WriteLine($"Nenhum aluno encontrado com o número '{studentNumber}'.");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erro ao remover aluno: " + e.Message);
            }
        }

        internal double GetAverageByStudentNumber(MySqlConnection connection, SchoolClass schoolClass, int studentNumber)
        {
            try
            {
                string query = $"SELECT average FROM {schoolClass.ToString().ToLower()} WHERE number = @number";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@number", studentNumber);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDouble(result);
                    }
                    else
                    {
                        Console.WriteLine($"Nenhum aluno encontrado com o número '{studentNumber}'.");
                        return 0.0;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erro ao obter a média do aluno: " + e.Message);
                return 0.0;
            }
        }

        internal List<string> GetNameAndAverageByRange(MySqlConnection connection, SchoolClass schoolClass, int numberOfTopStudents)
        {
            List<string> list = new List<string>();

            try
            {
                string query = $"SELECT name, average FROM {schoolClass.ToString().ToLower()} ORDER BY average DESC";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int count = 0;

                        while (reader.Read() && count < numberOfTopStudents)
                        {
                            string name = reader["name"].ToString();
                            double average = Convert.ToDouble(reader["average"]);

                            string studentInfo = $"{name} - {average}";

                            if (!list.Contains(studentInfo))
                            {
                                list.Add(studentInfo);
                                count++;
                            }
                        }
                    }
                }

                if (list.Count == 0)
                    Console.WriteLine($"Nenhum aluno encontrado na turma {schoolClass}.");

            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erro ao obter detalhes por faixa: " + e.Message);
            }

            return list;
        }


        internal int GetRowCount(MySqlConnection connection, SchoolClass schoolClass)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM {schoolClass.ToString().ToLower()}";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Erro ao obter o número de linhas na tabela {schoolClass}: {e.Message}");
                return -1;
            }
        }
    }
}
