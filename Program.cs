using SchoolQuery.backend;
using SchoolQuery.backend.query;
using MySql.Data.MySqlClient;
using SchoolQuery.util;

namespace SchoolQuery
{

    class Program
    {

        public static string MENU_MESSAGE = "1.Inserir Aluno, 2. Remover Aluno, 3. Pegar Média, 4. Top 10 Médias, 5. Sair";


        static void GetMenu(MySqlConnection connection, int choose)
        {

            SqlQuery query = new SqlQuery();

            switch (choose)
            {
                case 1:
                    Console.WriteLine("Turmas: " + Util.GetSchoolClasses());
                    Console.WriteLine("Insira a turma: ");

                    string classInput = Console.ReadLine();

                    if (Enum.TryParse(classInput.ToUpper(), true, out SchoolClass schoolClass) && !Util.isNumber(classInput))
                    {
                        Console.WriteLine("Insira o nome inteiro o(a) aluno(a) a adicionar:");
                        string studentToAdd = Console.ReadLine();

                        Console.WriteLine("Insira o número o(a) aluno(a) " + studentToAdd + ":");
                        int.TryParse(Console.ReadLine(), out int studentNumber);

                        Console.WriteLine("Insira a média das notas do(a) aluno(a) " + studentToAdd + ":");
                        double.TryParse(Console.ReadLine(), out double studentAverage);

                        try
                        {
                            query.AddStudent(connection, schoolClass, studentNumber, studentToAdd, studentAverage);
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("Não foi possivel inserir o aluno no banco de dados.\n" + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Turma não enconrada.");
                    }
                    break;

                case 2:
                    Console.WriteLine("Turmas: " + Util.GetSchoolClasses());
                    Console.WriteLine("Insira a turma: ");

                    classInput = Console.ReadLine();

                    if (Enum.TryParse(classInput.ToUpper(), true, out schoolClass) && !Util.isNumber(classInput))
                    {

                        Console.WriteLine("Insira o número do(a) aluno(a) a remover:");
                        int.TryParse(Console.ReadLine(), out int numberToRemove);

                        try
                        {
                            query.RemoveStudent(connection, schoolClass, numberToRemove);
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("Não foi possivel remover o aluno no banco de dados.\n" + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Turma não enconrada.");
                    }

                    break;

                case 3:
                    Console.WriteLine("Turmas: " + Util.GetSchoolClasses());
                    Console.WriteLine("Insira a turma: ");

                    classInput = Console.ReadLine();

                    if (Enum.TryParse(classInput.ToUpper(), true, out schoolClass) && !Util.isNumber(classInput))
                    {

                        Console.WriteLine("Insira o número do(a) aluno(a) a ver a média:");
                        int.TryParse(Console.ReadLine(), out int studentNumberAverage);

                        try
                        {
                            Console.WriteLine("A média do aluno número número " + studentNumberAverage + " é " + query.GetAverageByStudentNumber(connection, schoolClass, studentNumberAverage));
                        }
                        catch (MySqlException e)
                        {
                            Console.WriteLine("Não foi possivel pegar a média de um aluno no banco de dados.\n" + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Turma não enconrada.");
                    }

                    break;

                case 4:
                    Console.WriteLine("Turmas: " + Util.GetSchoolClasses());
                    Console.WriteLine("Insira a turma: ");

                    classInput = Console.ReadLine();

                    if (Enum.TryParse(classInput.ToUpper(), true, out schoolClass) && !Util.isNumber(classInput))
                    {

                        try
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("TOP 10 Médias - " + schoolClass.ToString().Replace("_", " "));

                            foreach (string average in query.GetNameAndAverageByRange(connection, schoolClass, 10))
                            {
                                Console.WriteLine(average);
                            }

                            Console.WriteLine("\n");
                        }

                        catch (MySqlException e)
                        {
                            Console.WriteLine("Não foi possivel pegar a média de um aluno no banco de dados.\n" + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Turma não enconrada.");
                    }

                    break;
            }
        }

        static void Main()
        {
            Connection connection = new Connection("rodriaum", "rodriaum", "12345");

            SqlQuery query = new SqlQuery();
            query.CreateTablesIfNotExists(connection.MySqlConnection);

            int option = -1;

            while (option != 5)
            {

                Console.WriteLine("\n" + Util.GetHyphenfromNumber());
                Console.WriteLine(MENU_MESSAGE);
                Console.WriteLine(Util.GetHyphenfromNumber() + "\n");

                Console.WriteLine("Insira a opção: ");
                int.TryParse(Console.ReadLine(), out option);

                GetMenu(connection.MySqlConnection, option);
            }

            Console.WriteLine("Você saiu do programa escolar.");

        }
    }
}
