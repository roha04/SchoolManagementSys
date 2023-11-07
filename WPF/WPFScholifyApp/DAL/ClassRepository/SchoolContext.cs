using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFScholifyApp.DAL.DBClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WPFScholifyApp.DAL.ClassRepository
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }
        public SchoolContext()
        {
            Database.EnsureCreated();   
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ScholifyDB;Username=postgres;Password=slava123");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Parents> Parents { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<DayBook> DayBooks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }

    public class SchoolContextFactory : IDesignTimeDbContextFactory<SchoolContext>
    {
        public SchoolContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
            optionsBuilder.UseNpgsql("Data Source=ScholifyDB.db");

            return new SchoolContext(optionsBuilder.Options);
        }
    }
}
