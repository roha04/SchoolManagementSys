// <copyright file="SchoolContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.DAL.ClassRepository
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using WPFScholifyApp.DAL.DBClasses;

    public class SchoolContext : DbContext
    {
        private readonly EnumsTime times = new EnumsTime();

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public SchoolContext()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<User>? Users { get; set; }

        public DbSet<Admin>? Admins { get; set; }

        public DbSet<Teacher>? Teachers { get; set; }

        public DbSet<Pupil>? Pupils { get; set; }

        public DbSet<Parents>? Parents { get; set; }

        public DbSet<Class>? Classes { get; set; }

        public DbSet<Schedule>? Schedules { get; set; }

        public DbSet<DayBook>? DayBooks { get; set; }

        public DbSet<Subject>? Subjects { get; set; }

        public DbSet<DBClasses.DayOfWeek>? DayOfWeeks { get; set; }

        public DbSet<LessonTime>? LessonTimes { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ScholifyTEST;Username=postgres;Password=slava123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
            .HasIndex(x => x.UserId)
            .IsUnique(false);

            modelBuilder.Entity<ParentsPupil>()
                .HasKey(pt => new { pt.PupilId, pt.ParentId });

            modelBuilder.Entity<ParentsPupil>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.ParentsPupils)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ParentsPupil>()
                .HasOne(x => x.Pupil)
                .WithMany(x => x.ParentsPupil)
                .HasForeignKey(x => x.PupilId)
                .OnDelete(DeleteBehavior.Cascade);

            // Додаємо тестові дані при створені бази даних
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "adminschool@gmail.com", Password = "Admin16", FirstName = "Іван", LastName = "Савчук", MiddleName = "Богданович", Birthday = new DateTime(1990, 11, 25).ToUniversalTime(), Role = "адмін", Address = "вул. Перемоги, 1", Gender = "чоловік", PhoneNumber = "0635487665" },
                new User { Id = 2, Email = "viktoriahalyshko@gmail.com", Password = "HV120190", FirstName = "Вікторія", LastName = "Галушко", MiddleName = "Ігорівна", Birthday = new DateTime(1995, 10, 20).ToUniversalTime(), Role = "вчитель", Address = "вул. Героїв Крут, 2", Gender = "жінка", PhoneNumber = "0965656789" },
                new User { Id = 3, Email = "igorhnatyuk@gmail.com", Password = "IH161008", FirstName = "Ігор", LastName = "Гнатюк", MiddleName = "Іванович", Birthday = new DateTime(1986, 08, 16).ToUniversalTime(), Role = "вчитель", Address = "вул. Зелена, 3", Gender = "чоловік", PhoneNumber = "0687904532" },
                new User { Id = 4, Email = "olegvynnyk@gmail.com", Password = "OV132678", FirstName = "Олег", LastName = "Винник", MiddleName = "Павлович", Birthday = new DateTime(1987, 06, 27).ToUniversalTime(), Role = "вчитель", Address = "вул Садова, 4", Gender = "чоловік", PhoneNumber = "0967894532" },
                new User { Id = 5, Email = "muhailoivashenko@gmail.com", Password = "MV123456", FirstName = "Михайло", LastName = "Іващенко", MiddleName = "Святославович", Birthday = new DateTime(2012, 10, 25).ToUniversalTime(), Gender = "чоловік", Address = "вул. Дорошенка, 5", PhoneNumber = "0685495126", Role = "учень" },
                new User { Id = 6, Email = "svitlanaromanyuk@gmail.com", Password = "SR564321", FirstName = "Світлана", LastName = "Романюк", MiddleName = "Василівна", Birthday = new DateTime(2011, 01, 10).ToUniversalTime(), Gender = "жінка", Address = "вул. Миру,8", PhoneNumber = "0635472856", Role = "учень" },
                new User { Id = 7, Email = "ivanivanov@gmail.com", Password = "II780965", FirstName = "Іван", LastName = "Іванов", MiddleName = "Іванович", Birthday = new DateTime(2012, 05, 23).ToUniversalTime(), Role = "учень", Address = "вул. Соборна, 12", Gender = "чоловік", PhoneNumber = "0654123467" },
                new User { Id = 8, Email = "olenapetrenko@gmail.com", Password = "OP785634", FirstName = "Олена", LastName = "Петренко", MiddleName = "Володимирівна", Birthday = new DateTime(2012, 05, 16).ToUniversalTime(), Role = "учень", Address = "вул. Сербська, 13", Gender = "жінка", PhoneNumber = "0984532121" },
                new User { Id = 9, Email = "maxsymsemenyuk@gmail.com", Password = "MS451209", FirstName = "Максим", LastName = "Семенов", MiddleName = "Віталійович", Birthday = new DateTime(2011, 12, 30).ToUniversalTime(), Role = "учень", Address = "вул. Пекарська, 14", Gender = "чоловік", PhoneNumber = "0987543218" },
                new User { Id = 10, Email = "svyatoslavivashenko@gmail.com", Password = "SI129007", FirstName = "Святослав", LastName = "Іващенко", MiddleName = "Андрійович", Birthday = new DateTime(2023, 11, 25).ToUniversalTime(), Role = "батьки", Address = "вул. Дорошенка, 5", Gender = "чоловік", PhoneNumber = "0987543218" },
                new User { Id = 11, Email = "vasuliyromanyuk@gmail.com", Password = "VR621094", FirstName = "Василій", LastName = "Романюк", MiddleName = "Олександрович", Birthday = new DateTime(2023, 11, 25).ToUniversalTime(), Role = "батьки", Address = "вул. Миру,8", Gender = "чоловік", PhoneNumber = "0987543218" },
                new User { Id = 12, Email = "marynaivanova@gmail.com", Password = "MI941806", FirstName = "Марина", LastName = "Іванова", MiddleName = "Василівна", Birthday = new DateTime(2023, 11, 25).ToUniversalTime(), Role = "батьки", Address = "вул. Соборна, 12", Gender = "жінка", PhoneNumber = "0984532121" },
                new User { Id = 13, Email = "volodymyrpetrenko@gmail.com", Password = "VP564712", FirstName = "Володимир", LastName = "Петренко", MiddleName = "Михайлович", Birthday = new DateTime(2023, 11, 25).ToUniversalTime(), Role = "батьки", Address = "вул. Сербська, 13", Gender = "чоловік", PhoneNumber = "0987543218" },
                new User { Id = 14, Email = "tetianasemenov@gmail.com", Password = "TS309128", FirstName = "Тетяна", LastName = "Семенов", MiddleName = "Андріївна", Birthday = new DateTime(2023, 11, 25).ToUniversalTime(), Role = "батьки", Address = "вул. Пекарська, 14", Gender = "жінка", PhoneNumber = "0984532121" });

            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1 });

            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, ClassName = "5-А" },
                new Class { Id = 2, ClassName = "5-Б" },
                new Class { Id = 3, ClassName = "5-В" },
                new Class { Id = 4, ClassName = "5-Г" },
                new Class { Id = 5, ClassName = "6-А" },
                new Class { Id = 6, ClassName = "6-Б" },
                new Class { Id = 7, ClassName = "6-В" },
                new Class { Id = 8, ClassName = "6-Г" },
                new Class { Id = 9, ClassName = "7-А" },
                new Class { Id = 10, ClassName = "7-Б" },
                new Class { Id = 11, ClassName = "7-В" },
                new Class { Id = 12, ClassName = "7-Г" },
                new Class { Id = 13, ClassName = "8-А" },
                new Class { Id = 14, ClassName = "8-Б" },
                new Class { Id = 15, ClassName = "8-В" },
                new Class { Id = 16, ClassName = "8-Г" },
                new Class { Id = 17, ClassName = "9-А" },
                new Class { Id = 18, ClassName = "9-Б" },
                new Class { Id = 19, ClassName = "9-В" },
                new Class { Id = 20, ClassName = "9-Г" },
                new Class { Id = 21, ClassName = "10-А" },
                new Class { Id = 22, ClassName = "10-Б" },
                new Class { Id = 23, ClassName = "10-В" },
                new Class { Id = 24, ClassName = "10-Г" },
                new Class { Id = 25, ClassName = "11-А" },
                new Class { Id = 26, ClassName = "11-Б" },
                new Class { Id = 27, ClassName = "11-В" },
                new Class { Id = 28, ClassName = "11-Г" });

            modelBuilder.Entity<DBClasses.DayOfWeek>().HasData(
                new DBClasses.DayOfWeek { Id = 1, Day = "27.11.2023", Date = new DateTime(2023, 11, 27).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 2, Day = "28.11.2023", Date = new DateTime(2023, 11, 28).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 3, Day = "29.11.2023", Date = new DateTime(2023, 11, 29).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 4, Day = "30.11.2023", Date = new DateTime(2023, 11, 30).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 5, Day = "01.12.2023", Date = new DateTime(2023, 12, 1).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 6, Day = "02.12.2023", Date = new DateTime(2023, 12, 2).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 7, Day = "03.12.2023", Date = new DateTime(2023, 12, 3).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 8, Day = "04.12.2023", Date = new DateTime(2023, 12, 4).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 9, Day = "05.12.2023", Date = new DateTime(2023, 12, 5).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 10, Day = "06.12.2023", Date = new DateTime(2023, 12, 6).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 11, Day = "07.12.2023", Date = new DateTime(2023, 12, 7).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 12, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 13, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 14, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 15, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 16, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 17, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 18, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 19, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 20, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 21, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 22, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 23, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 24, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 25, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() },
                new DBClasses.DayOfWeek { Id = 26, Day = "08.12.2023", Date = new DateTime(2023, 12, 8).ToUniversalTime() });

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, SubjectName = "Алгебра", ClassId = 1 },
                new Subject { Id = 2, SubjectName = "Геометрія", ClassId = 2 },
                new Subject { Id = 3, SubjectName = "Географія", ClassId = 3 },
                new Subject { Id = 4, SubjectName = "Історія", ClassId = 4 },
                new Subject { Id = 5, SubjectName = "Укр. Мова", ClassId = 5 },
                new Subject { Id = 6, SubjectName = "Інформатика", ClassId = 6 },
                new Subject { Id = 7, SubjectName = "Хімія", ClassId = 7 },
                new Subject { Id = 8, SubjectName = "Біологія", ClassId = 8 },
                new Subject { Id = 9, SubjectName = "Фізика", ClassId = 9 },
                new Subject { Id = 10, SubjectName = "Основи здоров'я", ClassId = 10 },
                new Subject { Id = 11, SubjectName = "Захист Вітчизни", ClassId = 11 },
                new Subject { Id = 12, SubjectName = "Фізкультура", ClassId = 12 },
                new Subject { Id = 13, SubjectName = "Трудове навчання", ClassId = 13 },
                new Subject { Id = 14, SubjectName = "Правознавство", ClassId = 14 },
                new Subject { Id = 15, SubjectName = "Укр. література", ClassId = 15 },
                new Subject { Id = 16, SubjectName = "Зарубіжна літ.", ClassId = 16 },
                new Subject { Id = 17, SubjectName = "Англ. мова", ClassId = 17 },
                new Subject { Id = 18, SubjectName = "Нім. мова", ClassId = 18 },
                new Subject { Id = 19, SubjectName = "Музика", ClassId = 19 });

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 2, SubjectId = 1 },
                new Teacher { Id = 2, UserId = 3, SubjectId = 9 },
                new Teacher { Id = 3, UserId = 4, SubjectId = 14 });

            modelBuilder.Entity<Pupil>().HasData(
                new Pupil { Id = 5, UserId = 5, ClassId = 1 },
                new Pupil { Id = 6, UserId = 6, ClassId = 1 },
                new Pupil { Id = 7, UserId = 7, ClassId = 1 },
                new Pupil { Id = 8, UserId = 8, ClassId = 1 },
                new Pupil { Id = 9, UserId = 9, ClassId = 1 });

            modelBuilder.Entity<Parents>().HasData(
                new Parents { Id = 10, UserId = 10 },
                new Parents { Id = 11, UserId = 11 },
                new Parents { Id = 12, UserId = 12 },
                new Parents { Id = 13, UserId = 13 },
                new Parents { Id = 14, UserId = 14 });

            modelBuilder.Entity<ParentsPupil>().HasData(
                new ParentsPupil { ParentId = 10, PupilId = 5 },
                new ParentsPupil { ParentId = 11, PupilId = 6 },
                new ParentsPupil { ParentId = 12, PupilId = 7 },
                new ParentsPupil { ParentId = 13, PupilId = 8 },
                new ParentsPupil { ParentId = 14, PupilId = 9 });

            modelBuilder.Entity<LessonTime>().HasData(
                new LessonTime { Id = 1, Start = EnumsTime.T830.ToString("HH:mm"), End = EnumsTime.T915.ToString("HH:mm"), StartTime = EnumsTime.T830, EndTime = EnumsTime.T915 },
                new LessonTime { Id = 2, Start = EnumsTime.T930.ToString("HH:mm"), End = EnumsTime.T1015.ToString("HH:mm"), StartTime = EnumsTime.T930, EndTime = EnumsTime.T1015 },
                new LessonTime { Id = 3, Start = EnumsTime.T1030.ToString("HH:mm"), End = EnumsTime.T1115.ToString("HH:mm"), StartTime = EnumsTime.T1030, EndTime = EnumsTime.T1115 },
                new LessonTime { Id = 4, Start = EnumsTime.T1135.ToString("HH:mm"), End = EnumsTime.T1220.ToString("HH:mm"), StartTime = EnumsTime.T1135, EndTime = EnumsTime.T1220 },
                new LessonTime { Id = 5, Start = EnumsTime.T1240.ToString("HH:mm"), End = EnumsTime.T1325.ToString("HH:mm"), StartTime = EnumsTime.T1240, EndTime = EnumsTime.T1325 },
                new LessonTime { Id = 6, Start = EnumsTime.T1335.ToString("HH:mm"), End = EnumsTime.T1420.ToString("HH:mm"), StartTime = EnumsTime.T1335, EndTime = EnumsTime.T1420 },
                new LessonTime { Id = 7, Start = EnumsTime.T1435.ToString("HH:mm"), End = EnumsTime.T1520.ToString("HH:mm"), StartTime = EnumsTime.T1435, EndTime = EnumsTime.T1520 },
                new LessonTime { Id = 8, Start = EnumsTime.T1530.ToString("HH:mm"), End = EnumsTime.T1615.ToString("HH:mm"), StartTime = EnumsTime.T1530, EndTime = EnumsTime.T1615 });

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { Id = 1, TeacherId = 1, ClassId = 1, LessonTimeId = 1, DayOfWeekId = 1, SubjectId = 1 },
                new Schedule { Id = 2, TeacherId = 2, ClassId = 2, LessonTimeId = 1, DayOfWeekId = 1, SubjectId = 2 },
                new Schedule { Id = 3, TeacherId = 3, ClassId = 3, LessonTimeId = 2, DayOfWeekId = 2, SubjectId = 3 });

            modelBuilder.Entity<DayBook>().HasData(
                new DayBook { Id = 1, Grade = 10, PupilId = 5, Attendance = " ", ScheduleId = 1 },
                new DayBook { Id = 2, Grade = 9, PupilId = 6, Attendance = "н", ScheduleId = 2 },
                new DayBook { Id = 3, Grade = 8, PupilId = 7, Attendance = " ", ScheduleId = 3 },
                new DayBook { Id = 4, Grade = 10, PupilId = 8, Attendance = " ", ScheduleId = 1 },
                new DayBook { Id = 5, Grade = 7, PupilId = 9, Attendance = " ", ScheduleId = 2 },
                new DayBook { Id = 6, Grade = 9, PupilId = 5, Attendance = " ", ScheduleId = 3 },
                new DayBook { Id = 7, Grade = 8, PupilId = 6, Attendance = " ", ScheduleId = 1 },
                new DayBook { Id = 8, Grade = 10, PupilId = 7, Attendance = " ", ScheduleId = 2 },
                new DayBook { Id = 9, Grade = 7, PupilId = 8, Attendance = "н", ScheduleId = 3 },
                new DayBook { Id = 10, Grade = 8, PupilId = 9, Attendance = "н", ScheduleId = 1 });
        }
    }

    public class SchoolContextFactory : IDesignTimeDbContextFactory<SchoolContext>
    {
        public SchoolContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
            optionsBuilder.UseNpgsql("Data Source=ScholifyTEST.db");

            return new SchoolContext(optionsBuilder.Options);
        }
    }
}
