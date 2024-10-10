namespace Watchlist.Data
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public ICollection<MovieUser> MovieUsers { get; set; } = new List<MovieUser>();  // Relation avec MovieUser
    }
}
