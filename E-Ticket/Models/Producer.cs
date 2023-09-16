using E_Ticket.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Ticket.Models
{
    public class Producer: IEntityBase
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Display(Name = "Profile Picture URL")]
        public string Picture { get; set; }

        [Required, Display(Name = "Biography")]
        public string Bio { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
