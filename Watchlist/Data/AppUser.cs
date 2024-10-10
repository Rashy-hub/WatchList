using Microsoft.AspNetCore.Identity;
using Watchlist.Data;

public class AppUser : IdentityUser
{
    public AppUser() : base()
    {
        
        MovieList = new HashSet<MovieUser>();
    }

 
    public string firstname { get; set; } 

  
    public virtual ICollection<MovieUser> MovieList { get; private set; }


}
