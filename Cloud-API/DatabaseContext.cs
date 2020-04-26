using Cloud_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cloud_API
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<BodyPart> BodyParts { get; set; }

        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BodyPartExercise>().HasKey(bpe => new { bpe.BodyPartId, bpe.ExerciseId });
        }
    }
}
