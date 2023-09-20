using E_Ticket.Data.Services.IServices;
using E_Ticket.Data.ViewModels;
using E_Ticket.Models;
using eTicket.Data.Static;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Ticket.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service) => _service = service;

        public async Task<IActionResult> Users() => View(await _service.GetAllUsersAsync());

        public IActionResult Login() => View(new LoginVM());
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            ApplicationUser user = await _service.FindByEmailAsync(loginVM.Email);
            if (user != null)
                if (await _service.CheckPasswordAsync(user, loginVM.Password))
                    if ((await _service.PasswordSignInAsync(user, loginVM.Password)).Succeeded)
                        return RedirectToAction("Index", "Movies");

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);

            ApplicationUser user = await _service.FindByEmailAsync(registerVM.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                Name = registerVM.Name,
                Email = registerVM.Email
            };

            if ((await _service.CreateNewUserAsync(newUser, registerVM.Password)).Succeeded)
                await _service.AddRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _service.Logout();
            return RedirectToAction("Index", "Movies");
        }
    }
}
