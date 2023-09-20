using E_Ticket.Data.Cart;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Data.ViewModels;
using E_Ticket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Ticket.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index() => View(
            await _ordersService.GetOrdersByUserIdAndRoleAsync(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                User.FindFirstValue(ClaimTypes.Role)));

        public async Task<IActionResult> ShoppingCart()
        {
            _shoppingCart.ShoppingCartItems = await _shoppingCart.GetShoppingCartItemsAsync();
            return View(new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = await _shoppingCart.GetShoppingTotalAsync()
            });
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            Movie item = await _moviesService.GetByIdAsync(id);
            if (item != null)
                await _shoppingCart.AddItemToCartAsync(item);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            Movie item = await _moviesService.GetByIdAsync(id);
            if (item != null)
                await _shoppingCart.RemoveItemFromCartAsync(item);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            List<ShoppingCartItem> items = await _shoppingCart.GetShoppingCartItemsAsync();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmil = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmil);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
