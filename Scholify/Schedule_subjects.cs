using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class Schedule_subjects
    {
        private string connection = "Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;";

        private static HashSet<int> generatedSchedule_subjectsIds = new HashSet<int>();
        private static Random random = new Random();
        private static int GenerateRandomScheduleSubjectsId()
        {
            int maxScheduleSubjectId = 10000;

            while (true)
            {
                int randomScheduleSubjectsId = random.Next(1, maxScheduleSubjectId + 1);

                if (!generatedSchedule_subjectsIds.Contains(randomScheduleSubjectsId))
                {
                    generatedSchedule_subjectsIds.Add(randomScheduleSubjectsId);
                    return randomScheduleSubjectsId;
                }
            }
        }
        public List<int> GetAllScheduleIds(NpgsqlConnection connection)
        {
            List<int> allScheduleIds = new List<int>();
            using (NpgsqlCommand selectScheduleIdsCommand = new NpgsqlCommand("SELECT schedule_id FROM schedule", connection))
            using (NpgsqlDataReader reader = selectScheduleIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allScheduleIds.Add(reader.GetInt32(0));
                }
            }
            return allScheduleIds;
        }
        public List<int> GetAllSubjectsIds(NpgsqlConnection connection)
        {
            List<int> allSubjectIds = new List<int>();
            using (NpgsqlCommand selectSubjectIdsCommand = new NpgsqlCommand($"SELECT subject_id FROM subjects ", connection))
            using (NpgsqlDataReader reader = selectSubjectIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allSubjectIds.Add(reader.GetInt32(0));
                }
            }
            return allSubjectIds;
        }
        public static int GenerateRandomScheduleId(List<int> allScheduleIds)
        {

            var randomIndex = new Random().Next(allScheduleIds.Count);

            var randomScheduleId = allScheduleIds[randomIndex];

            return randomScheduleId;
        }
        public static int GenerateRandomSubjectId(List<int> allSubjectsIds)
        {
            var randomIndex = new Random().Next(allSubjectsIds.Count);

            var randomSubjectId = allSubjectsIds[randomIndex];

            return randomSubjectId;
        }
        public  void InsertScheduleSubjects(int numberOfScheduleSubjects)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {

                connection.Open();

                for (int i = 1; i <= numberOfScheduleSubjects; i++)
                {
                    int schedule_subject = GenerateRandomScheduleSubjectsId();
                    int schedule_id = GenerateRandomScheduleId(GetAllScheduleIds(connection));
                    int subject_id = GenerateRandomSubjectId(GetAllSubjectsIds(connection));




                    var command = new NpgsqlCommand("INSERT INTO schedule_subjects (schedule_subject_id,schedule_id,subject_id)" +
                        "VALUES (@schedule_subject,@schedule_id,@subject_id)", connection);

                    command.Parameters.AddWithValue("@schedule_subject", schedule_subject);
                    command.Parameters.AddWithValue("@schedule_id", schedule_id);
                    command.Parameters.AddWithValue("@subject_id", subject_id);


                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }
    }

}
