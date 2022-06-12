using Microsoft.EntityFrameworkCore;
using universityPlatform.Models.dataAccess;

namespace universityPlatform.dataAccess
{
    public class UniversityContext : DbContext
    {
        //Para hacer SeriLog
        private readonly ILoggerFactory _loggerFactory;

        public UniversityContext(DbContextOptions<UniversityContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        // Add DbSets (Tables of our Data base)
        public DbSet<Users> User { get; set; }
        public DbSet<Courses> Course { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Students> Student { get; set; }
        
    

        //para nuestra relacion de datos, varios con varios
        /*protected  override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<StudentCourse>().HasKey(x => new { x.coursesId, x.studentId });
             modelBuilder.Entity<CoursesCategories>().HasKey(x => new { x.coursebyId, x.categoriesbyId});
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<UniversityContext>();

            optionsBuilder.LogTo(d => logger.Log(LogLevel.Warning, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Warning)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            optionsBuilder.LogTo(d => logger.Log(LogLevel.Error, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Error)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            optionsBuilder.LogTo(d => logger.Log(LogLevel.Critical, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Critical)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
        }



    }
}
