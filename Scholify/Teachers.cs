using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class Teachers
    {
        private string connection = "Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;";

        private static HashSet<int> generatedTeacherIds = new HashSet<int>();
        private static string[] genders = { "чоловіча", "жіноча" };
        private static Random random = new Random();
        private static int GenerateRandomTeacherId()
        {
            int maxTeacherId = 10000;

            while (true)
            {
                int randomTeacherId = random.Next(1, maxTeacherId + 1);

                if (!generatedTeacherIds.Contains(randomTeacherId))
                {
                    generatedTeacherIds.Add(randomTeacherId);
                    return randomTeacherId;
                }
            }
        }
        private static string generateFirstName()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomString = "";

            for (int i = 0; i < characters.Length; i++)
            {
                randomString += characters[random.Next(characters.Length)];
            }

            return randomString;
        }
        private static string generateMiddleName()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomString = "";

            for (int i = 0; i < characters.Length; i++)
            {
                randomString += characters[random.Next(characters.Length)];
            }

            return randomString;
        }
        private static string generateLastName()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomString = "";

            for (int i = 0; i < characters.Length; i++)
            {
                randomString += characters[random.Next(characters.Length)];
            }

            return randomString;
        }
        private static string generateGender()
        {
            while (true)
            {
                int index_subjects = random.Next(genders.Length);
                string subject = genders[index_subjects];
                return subject;
            }
        }        
        public static DateTime GetBirthDate()
        {
            var minDate = new DateTime(1960, 01, 01);
            var maxDate = new DateTime(2000, 12, 31);

            var random = new Random();
            var ticks = random.NextInt64(minDate.Ticks, maxDate.Ticks + 1);
            return new DateTime(ticks);
        }

        public static string generateAddress() 
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string nums = "1234567890";
            string randomString = "";

            for (int i = 0; i < characters.Length; i++)
            {
                randomString += characters[random.Next(characters.Length)];
            }
            for(int i = 0; i < nums.Length; i++)
            {
                randomString += nums[random.Next(nums.Length)];
            }

            return randomString;
        }
        public static string generatePhoneNumber()
        {
            var phoneNumber = string.Format("{0:000}-{1:000}-{2:0000}", random.Next(1000), random.Next(1000), random.Next(10000));

            return phoneNumber;
        }
        public List<int> GetAllSubjectIds(NpgsqlConnection connection)
        {
            List<int> allSubjectIds = new List<int>();
            using (NpgsqlCommand selectSubjectIdsCommand = new NpgsqlCommand("SELECT subject_id FROM subjects", connection))
            using (NpgsqlDataReader reader = selectSubjectIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allSubjectIds.Add(reader.GetInt32(0));
                }
            }
            return allSubjectIds;
        }
        public List<int> GetAllUSerIds(NpgsqlConnection connection)
        {
            List<int> allUserIds = new List<int>();
            using (NpgsqlCommand selectUserIdsCommand = new NpgsqlCommand($"SELECT user_id FROM users {"WHERE role = 'teacher'"}", connection))
            using (NpgsqlDataReader reader = selectUserIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allUserIds.Add(reader.GetInt32(0));
                }
            }
            return allUserIds;
        }
        public static int GenerateRandomSubjectId(List<int> allSubjectIds)
        {

            var randomIndex = new Random().Next(allSubjectIds.Count);

            var randomSubjectId = allSubjectIds[randomIndex];

            return randomSubjectId;
        }
        public static int GenerateRandomUserId(List<int> allUserIds)
        {
            var randomIndex = new Random().Next(allUserIds.Count);

            var randomUserId = allUserIds[randomIndex];

            return randomUserId;
        }


        public void InsertTeachers(int numberOfClasses)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
                connection.Open();
               

                for (int i = 1; i <= numberOfClasses; i++)
                {
                    int teacher_id = GenerateRandomTeacherId();
                    string first_name = generateFirstName();
                    string middle_name = generateMiddleName();
                    string last_name = generateLastName();
                    string gender = generateGender();
                    DateTime birthday = GetBirthDate();
                    string address = generateAddress();
                    string phone_number = generatePhoneNumber();
                    int user_id = GenerateRandomUserId(GetAllUSerIds(connection));
                    int subject_id = GenerateRandomSubjectId(GetAllSubjectIds(connection));


                    var command = new NpgsqlCommand("INSERT INTO teachers (teacher_id,first_name,middle_name,last_name," +
                        "gender,birthday,address,phone_number,user_id,subject_id)" +
                        "VALUES (@teacher_id,@first_name,@middle_name,@last_name,@gender,@birthday,@address,@phone_number,@user_id,@subject_id)", connection);

                    command.Parameters.AddWithValue("@teacher_id", teacher_id);
                    command.Parameters.AddWithValue("@first_name", first_name);
                    command.Parameters.AddWithValue("@middle_name", middle_name);
                    command.Parameters.AddWithValue("@last_name", last_name);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@birthday", birthday);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@phone_number", phone_number);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@subject_id", subject_id);





                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }

    }
}
