using Microsoft.EntityFrameworkCore;

namespace Registrar.Api.Data
{
    public class RegistrarContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public RegistrarContext(DbContextOptions<RegistrarContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ForNpgsqlUseIdentityColumns();
    }
}
