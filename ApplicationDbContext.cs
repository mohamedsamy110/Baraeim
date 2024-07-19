using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Graduation_Project.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<User> User { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<PregnantUser> PregnantUsers { get; set; }

        public DbSet<vaccination> Vaccinations { get; set; }

        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<Development> Developments { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Homepregnant> Homepregnants { get; set; }

        public DbSet<HomeMother> HomeMothers { get; set; }

        public DbSet<DailyExercise> DailyExercises { get; set; }

        public DbSet<PredictionRecord> PredictionRecords { get; set; }


    }

}
