using E_Ticket.Data.Cart;
using E_Ticket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Ticket.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart) => _shoppingCart = shoppingCart;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ShoppingCartItem> items = await _shoppingCart.GetShoppingCartItemsAsync();
            return View(items.Count);
        }
    }
}
