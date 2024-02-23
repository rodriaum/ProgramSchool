using SchoolQuery.backend;
using SchoolQuery.backend.query;
using MySql.Data.MySqlClient;

namespace SchoolQuery
{

    class Program
    {

        static void GetMenu(MySqlConnection connection, int choose)
        {
            Console.WriteLine("\n1. Inserir Aluno, 2. Remover Aluno, 3. Pegar Média, 4. Top 10 Médias\n");

            SqlQuery query = new SqlQuery();

            switch(choose)
            {
                case 1:
                    Console.WriteLine("Insira a turma: ");
                    // Verifica se é possível converter.
                    if (!Enum.TryParse(Console.ReadLine().ToUpper(), true, out SchoolClass schoolClass))
                        throw new ArgumentException("Turma não encontrada.");

                    Console.WriteLine("Insira o nome inteiro o(a) aluno(a):");
                    string name = Console.ReadLine();

                    Console.WriteLine("Insira o número o(a) aluno(a) " + name + ":");
                    int.TryParse(Console.ReadLine(), out int number);

                    Console.WriteLine("Insira a média das notas do(a) aluno(a) " + name + ":");
                    double.TryParse(Console.ReadLine(), out double average);

                    try
                    {
                        query.AddStudent(connection, schoolClass, number, name, average);
                    } 
                    catch(MySqlException e)
                    {
                        Console.WriteLine("Não foi possivel inserir o aluno no banco de dados.\n" + e.Message);
                    }
                    break;
            }
        }


        static void Main()
        {
            Connection connection = new Connection("rodriaum", "rodriaum", "12345");

            SqlQuery query = new SqlQuery();
            query.CreateDatabasesIfNotExists(connection.MySqlConnection);

            Console.WriteLine("\n1. Inserir Aluno, 2. Remover Aluno, 3. Pegar Média, 4. Top 10 Médias\n");

            Console.WriteLine("Insira a opção: ");
            int.TryParse(Console.ReadLine(), out int option);

            GetMenu(connection.MySqlConnection, option);

        }
    }
}
