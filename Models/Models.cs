using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CourseService.Models
{
    public class CourseServiceContext : DbContext
    {      
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        public CourseServiceContext(DbContextOptions<CourseServiceContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
