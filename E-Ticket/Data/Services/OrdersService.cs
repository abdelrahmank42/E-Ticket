using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DataContext _context;

        public OrdersService(DataContext context) => _context = context;

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            List<Order> orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Movie).Include(o => o.User).ToListAsync();
            return userRole != "Admin" ? orders.Where(n => n.UserId == userId).ToList() : orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            Order order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach (ShoppingCartItem item in items)
            {
                OrderItem orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
