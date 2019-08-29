using Microsoft.EntityFrameworkCore;

namespace Registrar.Api.Data
{
    public class RegistrarContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=registrar.db");
        }
    }
}
