using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class Classes

    {
        private static HashSet<int> generatedClassesIds = new HashSet<int>();
        private static string[] classesLetters = { "А", "Б", "В", "Г","Д"};
        private static Random random = new Random();
        public void InsertClass(int numberOfClasses)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
                connection.Open();


                for (int i = 1; i <= numberOfClasses; i++)
                {
                    int randomId = generateClassID();
                    string randomName = generateClassName();


                    var command = new NpgsqlCommand("INSERT INTO classes (class_id,class_name)" +
                        "VALUES (@class_id,@class_name)", connection);

                    command.Parameters.AddWithValue("@class_id", randomId);
                    command.Parameters.AddWithValue("@class_name", randomName);


                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }
        public static int generateClassID()
        {
            int maxClassId = 100;

            while (true)
            {
                int randomClassId = random.Next(1, maxClassId);

                if (!generatedClassesIds.Contains(randomClassId))
                {
                    generatedClassesIds.Add(randomClassId);
                    return randomClassId;
                }
            }
        }
        private static List<string> generatedClassesNames = new List<string>();

        public static string generateClassName()
        {

            while (true)
            {
                int randnumber = random.Next(1, 11);
                int index_classes = random.Next(classesLetters.Length);
                string classLetter = classesLetters[index_classes];
                string stringNumber = randnumber.ToString();
                string className  =stringNumber+"-"+ classLetter;
                if (!generatedClassesNames.Contains(className))
                {
                    generatedClassesNames.Add(className);
                    return className;
                }
            }
        }
    }
}
