using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Watchlist.Data;

namespace Watchlist.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieUser> MoviesUser { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }


        // We have to use the API Fluent of EF because our associative entity has its own extra properties :)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<AppUser>().Ignore(u => u.MovieList);

            // Composite key for MovieUser
            modelBuilder.Entity<MovieUser>().HasKey(t => new { t.IdUser, t.IdMovie });

            // Relationship with AppUser
            modelBuilder.Entity<MovieUser>()
                .HasOne(mu => mu.User)
                .WithMany(u => u.MovieList)
                .HasForeignKey(mu => mu.IdUser);

            // Relationship with Movie
            modelBuilder.Entity<MovieUser>()
                .HasOne(mu => mu.Movie)
                .WithMany(m => m.MovieUsers)
                .HasForeignKey(mu => mu.IdMovie);
        }
    }

}

