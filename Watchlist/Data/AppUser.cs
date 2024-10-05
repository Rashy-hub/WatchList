using Microsoft.AspNetCore.Identity;

namespace Watchlist.Data
{
    public class AppUser: IdentityUser
    {

        public AppUser() : base()
        {
            this.MovieList = new HashSet<MovieUser>();
        }

        public string firstname { get; set; }
        public virtual ICollection<MovieUser> Movies { get; set; }
        public HashSet<MovieUser> MovieList { get; private set; }
    }
}
