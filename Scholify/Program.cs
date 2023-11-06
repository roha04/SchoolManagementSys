using System;
using System.Linq;
using System.Net;
using System.Reflection;
using Npgsql;
using Scholify;

namespace MyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5433;Database=Scholify;User Id=postgres;Password=Rohasuper25;"))
            {
                connection.Open();
                //----------------USERS--------------------------------------
                /*User user = new User();
                user.InsertUsers(50);

                var command = new NpgsqlCommand("SELECT * FROM users", connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                   
                   
                    Console.WriteLine(reader["user_id"]);
                    Console.WriteLine(reader["email"]);
                    Console.WriteLine(reader["password"]);
                    Console.WriteLine(reader["role"]);
                    Console.WriteLine("--------------");

                }*/
                //---------------SUBJECTS------------------------------------------
                //Subject subject = new Subject();
                //subject.InsertSubject(12);
                //---------------CLASSES------------------------------------------
                // Classes classes = new Classes();
                //classes.InsertClass(10);
                // Teachers teachers = new Teachers();
                /*foreach(int n in teachers.GetAllSubjectIds(connection))
                {
                    Console.WriteLine(n);
                }*/
                //---------------TEACHERS------------------------------------------
                //teachers.InsertTeachers(12);
                //---------------PUPILS------------------------------------------
                //Pupil pupil = new Pupil();
                // pupil.InsertPupils(35);
                //---------------PARENTS------------------------------------------
                //Parents parents = new Parents();
                //parents.InsertPupils(35);
                //---------------PUPIL_PARENTS------------------------------------------
                //PupilParents pupilParents = new PupilParents();
                //pupilParents.InsertPupilParents(35);
                //connection.Close();
                //Console.WriteLine(connection.State);
                //---------------ADMINS------------------------------------------
                //Admin admins = new Admin();
                //admins.InsertAdmins(10);
                //---------------SCHEDULES------------------------------------------
                //Schedule schedule = new Schedule();
                // schedule.InsertSchedules(10);
                //---------------TEACHERS_CLASSES------------------------------------------
                //TeacherClasses teacherClasses = new TeacherClasses();
                //teacherClasses.InsertTeacherClasses(10);
                //---------------SCHEDULE_SUBJECTS------------------------------------------
                //Schedule_subjects schedule_Subjects = new Schedule_subjects();
                // schedule_Subjects.InsertScheduleSubjects(10);
                //---------------DAYBOOK-----------------------------------
                //daybook dayBook = new DayBook();
                //dayBook.InsertDaybook(10);
                connection.Close();
                Console.WriteLine(connection.State);

            }
        }

        
        
        

    }
}
