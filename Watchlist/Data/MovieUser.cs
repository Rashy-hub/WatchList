namespace Watchlist.Data
{
    public class MovieUser
    {
        public string IdUser { get; set; }
        public int IdMovie { get; set; }
        public int Note { get; set; }
        public bool hasSeen { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Movie Film { get; set; }
    }
}
