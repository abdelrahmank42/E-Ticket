using E_Ticket.Controllers;
using E_Ticket.Data;
using E_Ticket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.Data.Cart
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DataContext _context { get; set; }
        public ShoppingCart(DataContext context) => _context = context;

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            var contextAccessor = services.GetRequiredService<IHttpContextAccessor>();
            ISession session = contextAccessor.HttpContext.Session;
            DataContext context = services.GetRequiredService<DataContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public async Task AddItemToCartAsync(Movie movie)
        {
            ShoppingCartItem shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
                _context.ShoppingCartItems.Add(new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                });
            else
                shoppingCartItem.Amount++;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCartAsync(Movie movie)
        {
            ShoppingCartItem shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
                if (shoppingCartItem.Amount > 0)
                    shoppingCartItem.Amount--;
                else
                    _context.ShoppingCartItems.Remove(shoppingCartItem);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync() => ShoppingCartItems ?? (ShoppingCartItems = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToListAsync());

        public async Task<double> GetShoppingTotalAsync() => await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).SumAsync();

        public async Task ClearShoppingCartAsync()
        {
            List<ShoppingCartItem> items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}