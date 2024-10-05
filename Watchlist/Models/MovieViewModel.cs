namespace Watchlist.Models
{
    public class MovieViewModel
    {       
         public int IdMovie { get; set; }
         public string Title { get; set; }
         public int Year { get; set; }
         public bool isInTheList { get; set; }
         public bool hasSeen { get; set; }
         public int? Rating { get; set; }
       
    }
}
