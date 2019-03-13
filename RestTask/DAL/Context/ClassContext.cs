using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ClassContext : DbContext
    {
        public ClassContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Ivan",
                    Surname = "Ivanov",
                    Form = 9,
                    AverageMark = 8.5
                }, 
                new Student
                {
                    Id = 2,
                    Name = "Petr",
                    Surname = "Petrov",
                    Form = 8,
                    AverageMark = 7.7
                });
        }
    }
}
