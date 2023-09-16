using E_Ticket.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Ticket.Models
{
    public class Cinema: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Display(Name = "Logo URL")]
        public string Logo { get; set; }
        [Required]
        public string Description { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
