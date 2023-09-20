using E_Ticket.Models;
using System.Collections.Generic;

namespace E_Ticket.Data.ViewModels
{
    public class MovieDropdownsVM
    {
        public MovieDropdownsVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }

        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
