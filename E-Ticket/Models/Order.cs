using E_Ticket.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Ticket.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
