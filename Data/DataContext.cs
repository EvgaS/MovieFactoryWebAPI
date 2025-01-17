using Microsoft.EntityFrameworkCore;
using MovieFactoryWebAPI.Models;

namespace MovieFactoryWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //other possible way for DBSet -->public DbSet<Movie> Movies  => Set<Movie>();
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Actor> Actors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
              .HasOne(u => u.Movie)
              .WithMany(c => c.Roles)
              .HasForeignKey(u => u.MovieInfoKey);

            modelBuilder.Entity<Role>()
             .HasOne(u => u.Actor)
             .WithMany(c => c.Roles)
             .HasForeignKey(u => u.ActorInfoKey);
        }
    }
}
