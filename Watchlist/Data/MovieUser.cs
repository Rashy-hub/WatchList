using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Data
{
    public class MovieUser
    {
        [Required]
        public string IdUser { get; set; }

        [Required]
        public int IdMovie { get; set; }

        public int Rating { get; set; }

        public bool hasSeen { get; set; }

        [ForeignKey("IdUser")]
        public virtual AppUser User { get; set; }

        [ForeignKey("IdMovie")]
        public virtual Movie Movie { get; set; }

    }
}
