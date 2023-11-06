using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholify
{
    public  class Subject
    {
        private static string[] SUBJECTS = {"Math","History","English","Chemistry","Physics","Ukrainian language",
                                        "Ukranian literature","Foreingh literature","Art","PE","Algebra","Geometry","Georaphics"};

    private static HashSet<int> generatedSubjectsIds = new HashSet<int>();
        private static Random random = new Random();
        public void InsertSubject(int numberOfSubjects)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
                connection.Open();
                
                
                    for (int i = 1; i <= numberOfSubjects; i++)
                    {
                        int randomId = generateSubjectID();
                        string randomSubject = generateSubjectName();
                       

                        var command = new NpgsqlCommand("INSERT INTO subjects (subject_id,subject_name)" +
                            "VALUES (@subject_id,@subject_name)", connection);

                        command.Parameters.AddWithValue("@subject_id", randomId);
                        command.Parameters.AddWithValue("@subject_name", randomSubject);
                        

                        command.ExecuteNonQuery();
                    }
                
                connection.Close();

            }
        }
        public static int generateSubjectID()
        {
            int maxSubjectId = SUBJECTS.Length;

            while (true)
            {
                int randomSubjectId = random.Next(1, maxSubjectId );

                if (!generatedSubjectsIds.Contains(randomSubjectId))
                {
                    generatedSubjectsIds.Add(randomSubjectId);
                    return randomSubjectId;
                }
            }
        }
        private static List<string> generatedSubjectNames = new List<string>();

        public static string generateSubjectName()
        {
           
            while (true)
            {
                int index_subjects = random.Next(SUBJECTS.Length);
                string subject = SUBJECTS[index_subjects];
                if (!generatedSubjectNames.Contains(subject))
                {
                    generatedSubjectNames.Add(subject);
                    return subject;
                }
            }
        }
    }
}
