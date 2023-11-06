using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class TeacherClasses
    {
        private string connection = "Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;";

        private static HashSet<int> generatedTeacherClassesIds = new HashSet<int>();
        private static Random random = new Random();
        private static int GenerateRandomTeacherClassesId()
        {
            int maxTeacherClassesId = 10000;

            while (true)
            {
                int randomTeacherClassesId = random.Next(1, maxTeacherClassesId + 1);

                if (!generatedTeacherClassesIds.Contains(randomTeacherClassesId))
                {
                    generatedTeacherClassesIds.Add(randomTeacherClassesId);
                    return randomTeacherClassesId;
                }
            }
        }
        public List<int> GetAllTeachersIds(NpgsqlConnection connection)
        {
            List<int> allTeacherIds = new List<int>();
            using (NpgsqlCommand selectTeacherIdsCommand = new NpgsqlCommand("SELECT teacher_id FROM teachers", connection))
            using (NpgsqlDataReader reader = selectTeacherIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allTeacherIds.Add(reader.GetInt32(0));
                }
            }
            return allTeacherIds;
        }
        public List<int> GetAllClassesIds(NpgsqlConnection connection)
        {
            List<int> allClassesIds = new List<int>();
            using (NpgsqlCommand selectClassesIdsCommand = new NpgsqlCommand($"SELECT class_id FROM classes ", connection))
            using (NpgsqlDataReader reader = selectClassesIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allClassesIds.Add(reader.GetInt32(0));
                }
            }
            return allClassesIds;
        }
        public static int GenerateRandomClassId(List<int> allClassesIds)
        {

            var randomIndex = new Random().Next(allClassesIds.Count);

            var randomClassId = allClassesIds[randomIndex];

            return randomClassId;
        }
        public static int GenerateRandomTeacherId(List<int> allTeachersIds)
        {
            var randomIndex = new Random().Next(allTeachersIds.Count);

            var randomTeacherId = allTeachersIds[randomIndex];

            return randomTeacherId;
        }
        public void InsertTeacherClasses(int numberOfTeacherClasses)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
                connection.Open();


                for (int i = 1; i <= numberOfTeacherClasses; i++)
                {
                    int teacher_class_id = GenerateRandomTeacherClassesId();
                    int teacher_id = GenerateRandomTeacherId(GetAllTeachersIds(connection));
                    int class_id = GenerateRandomClassId(GetAllClassesIds(connection));




                    var command = new NpgsqlCommand("INSERT INTO teachers_classes (teacher_class_id,teacher_id,class_id)" +
                        "VALUES (@teacher_class_id,@teacher_id,@class_id)", connection);

                    command.Parameters.AddWithValue("@teacher_class_id", teacher_class_id);
                    command.Parameters.AddWithValue("@teacher_id", teacher_id);
                    command.Parameters.AddWithValue("@class_id", class_id);







                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }
        }

}
