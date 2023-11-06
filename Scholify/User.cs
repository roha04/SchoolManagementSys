using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public class User
    {
        private static string[] NAMES = { "liam", "olivia", "noah", "emma", "oliver", "ava", "elijah", "charlotte", "william", "yuriy",
            "james", "amelia", "benjamin", "isabella", "lucas", "mia", "henry", "harper", "jason", "evelyn", "michael", "abigail", "ethan",
            "emily", "daniel", "elizabeth", "matthew", "sofia", "aiden", "avery", "mason", "ella", "logan", "scarlett", "jackson", "grace",
            "sebastian", "chloe", "jack", "lily", "gabriel", "hannah", "caleb", "layla", "isaac", "victoria", "samuel", "madison", "david",
            "penelope", "joseph", "natalie", "john", "addison", "christopher", "zoe", "dylan", "taras", "joshua", "riley", "andrew", "savannah",
            "nicholas", "nora", "christopher", "zoey", "william", "leah", "jonathan", "audrey", "nathan", "claire", "adam", "grace", "ryan", "olena",
            "matthew", "stella", "justin", "violet", "brandon", "hazel", "jacob", "lucy", "kevin", "lillian", "evan", "liliana", "nicholas", "ostap",
            "aiden", "ruby", "dylan", "scarlet", "leo", "ivy", "carter", "eleanor", "connor", "samantha", "luke", "aubrey", "landon", "nevaeh", "christian",
            "layla", "isaiah", "madeline", "thomas", "alyssa", "jonathan", "serenity", "bryan", "skylar" };
        private static string[] LASTNAMES = { "smith", "johnson", "williams", "brown", "jones", "miller", "davis", "garcía", "rodriguez", "martínez",
            "hernández", "lópez", "gonzález", "wilson", "anderson", "thomas", "moore", "taylor", "lee", "harris", "clark", "lewis", "young", "hall",
            "walker", "white", "king", "turner", "perez", "wright", "adams", "scott", "mitchell", "ramos", "torres", "parker", "collins", "edwards", 
            "stewart", "sanchez", "morris", "rogers", "reed", "cook", "morgan", "bell", "murphy", "bailey", "rivera", "cooper", "richardson", "cox", 
            "howard", "ward", "torres", "peterson", "gray", "ramirez", "james", "watson", "brooks", "kelly", "sanders", "price", "bennett", "wood", 
            "barnes", "ross", "henderson", "coleman", "jenkins", "perry", "powell", "long", "patterson", "hughes", "flores", "washington", "butler",
            "simmons", "foster", "gonzales", "bryant", "melnyk", "russell", "griffin", "diaz", "hayes", "myers", "ford", "hamilton", "graham", 
            "sullivan", "wallace", "woods", "cole", "west", "jordan", "owens", "reynolds", "fisher", "ellis", "harrison", "gibson", "mcdonald",
            "cruz", "marshall", "ortiz", "gomez", "murray" };
        public  void InsertUsers(int numberOfUsers)
        {

            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
                connection.Open();

                for (int i = 1; i <= numberOfUsers; i++) 
                {
                    string randomEmail = GenerateRandomEmail();
                    string randomPassword = GenerateRandomPassword();
                    string randomRole = GenerateRandomRole();
                    int randomUserId = GenerateRandomUserId();

                    var command = new NpgsqlCommand("INSERT INTO users (user_id,email, password, role)" +
                        "VALUES (@user_id, @email, @password, @role)", connection);

                    command.Parameters.AddWithValue("@user_id", randomUserId);
                    command.Parameters.AddWithValue("@email", randomEmail);
                    command.Parameters.AddWithValue("@password", randomPassword);
                    command.Parameters.AddWithValue("@role", randomRole);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private static HashSet<int> generatedUserIds = new HashSet<int>();
        private static Random random = new Random();

        private static int GenerateRandomUserId()
        {
            int maxUserId = 10000; 

            while (true)
            {
                int randomUserId = random.Next(1, maxUserId + 1);

                if (!generatedUserIds.Contains(randomUserId))
                {
                    generatedUserIds.Add(randomUserId);
                    return randomUserId;
                }
            }
        }
        private static List<string> generatedEmails = new List<string>();

        private static string GenerateRandomEmail()
        {
            while (true)
            {
                int index_names = random.Next(NAMES.Length);
                int index_lastnames = random.Next(LASTNAMES.Length);
                string email = NAMES[index_names] + LASTNAMES[index_lastnames] + "@example.com";

                if (!generatedEmails.Contains(email))
                {
                    generatedEmails.Add(email);
                    return email;
                }
            }
        }
        private static string GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
        private static string[] ROLES = { "teacher", "pupil", "parent", "admin" };
        private static string GenerateRandomRole()
        {
            while (true)
            {
                int index_genders = random.Next(ROLES.Length);
                string genders = ROLES[index_genders];
                return genders;
            }
        }

       

    }
}
