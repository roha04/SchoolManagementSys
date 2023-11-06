using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class DayBook
    {
        private string connection = "Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;";

        private static HashSet<int> generatedDayBookIds = new HashSet<int>();
        private static string[] attendance = { " н", " " }; 
        private static Random random = new Random();
        private static int GenerateRandomDayBookId()
        {
            int maxDayId = 10000;

            while (true)
            {
                int randomDayBookId = random.Next(1, maxDayId + 1);

                if (!generatedDayBookIds.Contains(randomDayBookId))
                {
                    generatedDayBookIds.Add(randomDayBookId);
                    return randomDayBookId;
                }
            }
        }
        private static int generateGrade()
        {
            return random.Next(1, 12);
        }
        public static string SelectRandomItemAttendance()
        {
            while (true)
            {
                int index_attendance = random.Next(attendance.Length);
                string attendancee = attendance[index_attendance];
                return attendancee;
            }
        }
        public static DateTime GetLessonDate()
        {
            var minDate = new DateTime(2023, 09, 01);
            var maxDate = new DateTime(2023, 12, 16);

            var random = new Random();
            var ticks = random.NextInt64(minDate.Ticks, maxDate.Ticks + 1);
            return new DateTime(ticks);
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
        public static int GenerateRandomSubjectId(List<int> allSubjectsIds)
        {
            var randomIndex = new Random().Next(allSubjectsIds.Count);

            var randomUserId = allSubjectsIds[randomIndex];

            return randomUserId;
        }
        public List<int> GetAllClassesIds(NpgsqlConnection connection)
        {
            List<int> allPupilIds = new List<int>();
            using (NpgsqlCommand selectClassesIdsCommand = new NpgsqlCommand($"SELECT class_id FROM classes ", connection))
            using (NpgsqlDataReader reader = selectClassesIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allPupilIds.Add(reader.GetInt32(0));
                }
            }
            return allPupilIds;
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

            var randomTeacherId = allTeachersIds[randomIndex];

            return randomTeacherId;
        }
        public void InsertDaybook(int numberOfDaybooks)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
                connection.Open();


                for (int i = 1; i <= numberOfDaybooks; i++)
                {
                    int daybook_id = GenerateRandomDayBookId();
                    int grade = generateGrade();
                    string attendance = SelectRandomItemAttendance();
                    DateTime day = GetLessonDate();
                    int subject_id = GenerateRandomSubjectId(GetAllSubjectsIds(connection));
                    int teacher_id = GenerateRandomTeacherId(GetAllTeacherIds(connection));
                    int class_id = GenerateRandomClassId(GetAllClassesIds(connection));


                    var command = new NpgsqlCommand("INSERT INTO daybook (daybook_id,grade,attendance,day,subject_id,teacher_id,class_id)" +
                        "VALUES (@daybook_id,@grade,@attendance,@day,@subject_id,@teacher_id,@class_id)", connection);

                    command.Parameters.AddWithValue("@daybook_id", daybook_id);
                    command.Parameters.AddWithValue("@grade", grade);
                    command.Parameters.AddWithValue("@attendance", attendance);
                    command.Parameters.AddWithValue("@day", day);
                    command.Parameters.AddWithValue("@subject_id", subject_id);
                    command.Parameters.AddWithValue("@teacher_id", teacher_id);
                    command.Parameters.AddWithValue("@class_id", class_id);





                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }
    }
}
