using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        // We have to use the API Fluent of EF because our associative entity has its own extra properties :)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This way we tell to EF to use composite primary key made of IdUser and IdMovie
            base.OnModelCreating(modelBuilder);
            // MovieList caused errors when mapping with EF core , i choose to ignore it for the moment 
            modelBuilder.Entity<AppUser>().Ignore(u => u.MovieList);
            modelBuilder.Entity<MovieUser>()
                .HasKey(t => new { t.IdUser, t.IdMovie });
        }
    }
}
