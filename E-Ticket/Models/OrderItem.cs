﻿using System.ComponentModel.DataAnnotations.Schema;

namespace E_Ticket.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}