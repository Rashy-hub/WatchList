namespace Watchlist.Data
{
    public class MovieUser
    {
        public string IdUser { get; set; }
        public int IdMovie { get; set; }
        public int Rating { get; set; }
        public bool hasSeen { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
