using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMovies.Models;

namespace TMovies.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<TvShowActor> TvShowActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);
        // Configure Movie-Actor many-to-many relationship
        modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.Actors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.Movie)
                .HasForeignKey(ma => ma.ActorId);

            // Configure TvShow-Actor many-to-many relationship
            modelBuilder.Entity<TvShowActor>()
                .HasKey(ta => new { ta.TvShowId, ta.ActorId });

            modelBuilder.Entity<TvShowActor>()
                .HasOne(ta => ta.TvShow)
                .WithMany(t => t.Actors)
                .HasForeignKey(ta => ta.TvShowId);

            modelBuilder.Entity<TvShowActor>()
                .HasOne(ta => ta.Actor)
                .WithMany(a => a.TvShow)
                .HasForeignKey(ta => ta.ActorId);

            // Ensure unique index on MovieActor and TvShowActor
            modelBuilder.Entity<MovieActor>()
                .HasIndex(ma => new { ma.MovieId, ma.ActorId })
                .IsUnique();

            modelBuilder.Entity<TvShowActor>()
                .HasIndex(ta => new { ta.TvShowId, ta.ActorId })
                .IsUnique();
        }
    }
}
