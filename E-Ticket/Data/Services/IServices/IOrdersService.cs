using E_Ticket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Ticket.Data.Services.IServices
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
