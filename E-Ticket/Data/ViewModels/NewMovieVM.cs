using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using E_Ticket.Data.Enums;
using E_Ticket.Data.Base;

namespace E_Ticket.Data.ViewModels
{
    public class NewMovieVM 
    {
        [Required, Display(Name = "Movie name")]
        public string Name { get; set; }

        [Required, Display(Name = "Movie description")]
        public string Description { get; set; }

        [Required, Display(Name = "Price")]
        public double Price { get; set; }

        [Required, Display(Name = "Movie poster URL")]
        public string Poster { get; set; }

        [Required, Display(Name = "Movie start date")]
        public DateTime StartDate { get; set; }

        [Required, Display(Name = "Movie end date")]
        public DateTime EndDate { get; set; }

        [Required, Display(Name = "Select a category")]
        public Category Category { get; set; }

        //Relationships
        [Required, Display(Name = "Select actors")]
        public List<int> ActorIds { get; set; }

        [Required, Display(Name = "Select a cinema")]
        public int CinemaId { get; set; }

        [Required, Display(Name = "Select a producer")]
        public int ProducerId { get; set; }
    }
}
