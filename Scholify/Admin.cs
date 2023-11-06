using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class Admin
    {
        private string connection = "Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;";
        private static HashSet<int> generatedAdminIds = new HashSet<int>();
        private static Random random = new Random();
        private static int GenerateRandomAdminId()
        {
            int maxAdminId = 10000;

            while (true)
            {
                int randomAdminId = random.Next(1, maxAdminId + 1);

                if (!generatedAdminIds.Contains(randomAdminId))
                {
                if (!generatedAdminIds.Contains(randomAdminId))
                    generatedAdminIds.Add(randomAdminId);
                    return randomAdminId;
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
        public List<int> GetAllUserIds(NpgsqlConnection connection)
        {
            List<int> allUserIds = new List<int>();
            using (NpgsqlCommand selectUserIdsCommand = new NpgsqlCommand($"SELECT user_id FROM users {"WHERE role = 'admin'"}", connection))
            using (NpgsqlDataReader reader = selectUserIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allUserIds.Add(reader.GetInt32(0));
                }
            }
            return allUserIds;
        }

        public static int GenerateRandomUserId(List<int> allUserIds)
        {
            var randomIndex = new Random().Next(allUserIds.Count);

            var randomUserId = allUserIds[randomIndex];

            return randomUserId;
        }
        public void InsertAdmins(int numberOfAdmins)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
               


                for (int i = 1; i <= numberOfAdmins; i++)
                {
                    connection.Open();
                    int admin_id = GenerateRandomAdminId();
                    string first_name = generateFirstName();
                    string middle_name = generateMiddleName();
                    string last_name = generateLastName();
                    int user_id = GenerateRandomUserId(GetAllUserIds(connection));


                    var command = new NpgsqlCommand("INSERT INTO admins (admin_id,first_name,middle_name,last_name,user_id)" +
                        "VALUES (@admin_id,@first_name,@middle_name,@last_name,@user_id)", connection);

                    command.Parameters.AddWithValue("@admin_id", admin_id);
                    command.Parameters.AddWithValue("@first_name", first_name);
                    command.Parameters.AddWithValue("@middle_name", middle_name);
                    command.Parameters.AddWithValue("@last_name", last_name);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    

                    command.ExecuteNonQuery();
                    connection.Close();
                }

                connection.Close();

            }
        }
    }
}
