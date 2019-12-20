using Microsoft.EntityFrameworkCore;

namespace CourseService.Models
{
    public class CourseServiceDbContext : DbContext
    {
        public CourseServiceDbContext(DbContextOptions<CourseServiceDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>().HasKey(sc => new { sc.UserId, sc.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(en => en.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(en => en.CourseId);
            modelBuilder.Entity<Enrollment>()
                .HasOne(en => en.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(en => en.UserId);

            var cSharp = new Course { Id = 1, Name = "C# Programming" };
            var ruby = new Course { Id = 2, Name = "Ruby Programming" };
            var java = new Course { Id = 3, Name = "Java Programming" };

            modelBuilder.Entity<Course>().HasData(cSharp, ruby, java);

            var jack = new User { Id = 1, Email = "jack.tench@gmail.com" };
            var luke = new User { Id = 2, Email = "luke.lihou@gmail.com" };
            var graham = new User { Id = 3, Email = "graham.essau@gmail.com" };

            modelBuilder.Entity<User>().HasData(jack, luke, graham);

            var enrollment1 = new Enrollment { CourseId = cSharp.Id, UserId = jack.Id };
            var enrollment2 = new Enrollment { CourseId = ruby.Id, UserId = jack.Id };
            var enrollment3 = new Enrollment { CourseId = ruby.Id, UserId = luke.Id };
            var enrollment4 = new Enrollment { CourseId = java.Id, UserId = luke.Id };
            var enrollment5 = new Enrollment { CourseId = cSharp.Id, UserId = graham.Id };
            var enrollment6 = new Enrollment { CourseId = java.Id, UserId = graham.Id };

            modelBuilder.Entity<Enrollment>().HasData(enrollment1, enrollment2, enrollment3, enrollment4, enrollment5, enrollment6);
        }
    }
}
