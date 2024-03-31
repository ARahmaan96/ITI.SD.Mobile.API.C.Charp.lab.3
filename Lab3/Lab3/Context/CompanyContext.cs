using Lab3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Lab3.Context
{
    public class CompanyContext : IdentityDbContext<ApplicationUser>
    {
        public CompanyContext(DbContextOptions option) : base(option) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasMany(S => S.Courses).WithMany(C => C.Students).UsingEntity(j => j.ToTable("StudentCourses"));
        }


    }
}
