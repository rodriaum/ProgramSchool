using SchoolQuery.backend;
using System.Text;
using System.Text.RegularExpressions;

namespace SchoolQuery.util
{
    internal class Util
    {
        public static string GetHyphenfromNumber(int amount)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < amount; i++)
                builder.Append("-");

            return builder.ToString();

        }

        public static string GetHyphenfromNumber()
        {
            return GetHyphenfromNumber(Program.MENU_MESSAGE.Length);
        }

        public static SchoolClass[] GetListSchoolClass()
        {
            SchoolClass[] list = (SchoolClass[])Enum.GetValues(typeof(SchoolClass));
            return list;
        }


        public static string GetSchoolClasses()
        {
            StringBuilder builder = new StringBuilder();

            bool end = false;
            int index = 1;

            foreach (SchoolClass s in GetListSchoolClass())
            {
                if (index >= GetListSchoolClass().Length) end = true;

                builder.Append(s.ToString().ToLower());
                builder.Append(end ? "." : ", ");

                index++;
            }

            return builder.ToString().Trim();
        }

        public static bool isNumber(string s)
        {
            return Regex.IsMatch(s, @"^\d+$");
        }
    }
}
