using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class Schedule
    {
        private static string[] timeslots = {
    "08:00 - 08:45",
    "08:50 - 09:35",
    "09:40 - 10:25",
    "10:30 - 11:15",
    "11:20 - 12:05",
    "12:10 - 12:55",
    "13:00 - 13:45",
    "13:50 - 14:35",
    "14:40 - 15:25",
    "15:30 - 16:15",
    "16:20 - 17:05"
        };
        private static string[] days_of_week = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        private string connection = "Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;";
        private static HashSet<int> generatedScheduleIds = new HashSet<int>();
        private static Random random = new Random();
        private static int GenerateRandomScheduleId()
        {
            int maxAdminId = 10000;

            while (true)
            {
                int randomScheduleId = random.Next(1, maxAdminId + 1);

                if (!generatedScheduleIds.Contains(randomScheduleId))
                {
                    if (!generatedScheduleIds.Contains(randomScheduleId))
                        generatedScheduleIds.Add(randomScheduleId);
                    return randomScheduleId;
                }
            }
        }
        public static string SelectRandomItem(string[] list)
        {
            var random = new Random();
            int index = random.Next(list.Length-1);
            return list[index];
        }
        public List<int> GetAllClassesIds(NpgsqlConnection connection)
        {
            List<int> allClassesIds = new List<int>();
            using (NpgsqlCommand selectSubjectIdsCommand = new NpgsqlCommand("SELECT class_id FROM classes", connection))
            using (NpgsqlDataReader reader = selectSubjectIdsCommand.ExecuteReader())
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
        public List<int> GetAllTeacherIds(NpgsqlConnection connection)
        {
            List<int> allTeachersIds = new List<int>();
            using (NpgsqlCommand selectSubjectIdsCommand = new NpgsqlCommand("SELECT teacher_id FROM teachers", connection))
            using (NpgsqlDataReader reader = selectSubjectIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allTeachersIds.Add(reader.GetInt32(0));
                }
            }
            return allTeachersIds;
        }
        public static int GenerateRandomTeacherId(List<int> allTeachersIds)
        {

            var randomIndex = new Random().Next(allTeachersIds.Count);

            var randomClassId = allTeachersIds[randomIndex];

            return randomClassId;
        }
        public void InsertSchedules(int numberOfSchedules)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {



                for (int i = 1; i <= numberOfSchedules; i++)
                {
                    connection.Open();
                    int admin_id = GenerateRandomScheduleId();
                    string days = SelectRandomItem(days_of_week);
                    string times = SelectRandomItem(timeslots);
                    int teacher_id = GenerateRandomTeacherId(GetAllTeacherIds(connection));
                    int class_id = GenerateRandomClassId(GetAllClassesIds(connection));


                    var command = new NpgsqlCommand("INSERT INTO schedule (schedule_id,day_of_week,timeslot,teacher_id,class_id)" +
                        "VALUES (@schedule_id,@day_of_week,@timeslot,@teacher_id,@class_id)", connection);

                    command.Parameters.AddWithValue("@schedule_id", admin_id);
                    command.Parameters.AddWithValue("@day_of_week", days);
                    command.Parameters.AddWithValue("@timeslot", times);
                    command.Parameters.AddWithValue("@teacher_id", teacher_id);
                    command.Parameters.AddWithValue("@class_id", class_id);


                    command.ExecuteNonQuery();
                    connection.Close();
                }

                connection.Close();

            }
        }



    }
}
