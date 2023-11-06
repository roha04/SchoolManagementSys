using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class PupilParents
    {
        private string connection = "Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;";

        private static HashSet<int> generatedParentPupilIds = new HashSet<int>();
        private static Random random = new Random();
        private static int GenerateRandomParentPupilId()
        {
            int maxPupilId = 10000;

            while (true)
            {
                int randomParentsPuilId = random.Next(1, maxPupilId + 1);

                if (!generatedParentPupilIds.Contains(randomParentsPuilId))
                {
                    generatedParentPupilIds.Add(randomParentsPuilId);
                    return randomParentsPuilId;
                }
            }
        }
        public List<int> GetAllParentsIds(NpgsqlConnection connection)
        {
            List<int> allParentsIds = new List<int>();
            using (NpgsqlCommand selectParentsIdsCommand = new NpgsqlCommand("SELECT parents_id FROM parents", connection))
            using (NpgsqlDataReader reader = selectParentsIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allParentsIds.Add(reader.GetInt32(0));
                }
            }
            return allParentsIds;
        }
        public List<int> GetAllPupilsIds(NpgsqlConnection connection)
        {
            List<int> allPupilIds = new List<int>();
            using (NpgsqlCommand selectPupilIdsCommand = new NpgsqlCommand($"SELECT pupil_id FROM pupils ", connection))
            using (NpgsqlDataReader reader = selectPupilIdsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    allPupilIds.Add(reader.GetInt32(0));
                }
            }
            return allPupilIds;
        }
        public static int GenerateRandomParentId(List<int> allSubjectIds)
        {

            var randomIndex = new Random().Next(allSubjectIds.Count);

            var randomSubjectId = allSubjectIds[randomIndex];

            return randomSubjectId;
        }
        public static int GenerateRandomPupilId(List<int> allUserIds)
        {
            var randomIndex = new Random().Next(allUserIds.Count);

            var randomUserId = allUserIds[randomIndex];

            return randomUserId;
        }
        public void InsertPupilParents(int numberOfPupilParents)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
                connection.Open();


                for (int i = 1; i <= numberOfPupilParents; i++)
                {
                    int parent_pupils__id = GenerateRandomParentPupilId();
                    int parent_id = GenerateRandomParentId(GetAllParentsIds(connection));
                    int pupil_id = GenerateRandomPupilId(GetAllPupilsIds(connection));




                    var command = new NpgsqlCommand("INSERT INTO pupils_parents (pupil_parents_id,pupil_id,parents_id)" +
                        "VALUES (@pupil_parents_id,@pupil_id,@parents_id)", connection);

                    command.Parameters.AddWithValue("@pupil_parents_id", parent_pupils__id);
                    command.Parameters.AddWithValue("@pupil_id", pupil_id);
                    command.Parameters.AddWithValue("@parents_id", parent_id);







                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }
    }
}
