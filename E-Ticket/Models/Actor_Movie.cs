using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Ticket.Models
{
    public class Actor_Movie
    {
        [ForeignKey(nameof(Movie))]
        public int MovieId{ get; set; }
        public Movie Movie { get; set; }
        
        [ForeignKey(nameof(Actor))]
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
