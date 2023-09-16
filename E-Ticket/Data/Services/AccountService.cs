using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Ticket.Data.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _context;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync() => await _context.Users.ToListAsync();
        public async Task<ApplicationUser> FindByEmailAsync(string Email) => await _userManager.FindByEmailAsync(Email);
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string pass) => await _userManager.CheckPasswordAsync(user, pass);
        public async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string pass) => await _signInManager.PasswordSignInAsync(user, pass, false, false);

        public async Task<IdentityResult> CreateNewUserAsync(ApplicationUser newUser, string pass) => await _userManager.CreateAsync(newUser, pass);
        public async Task<IdentityResult> AddRoleAsync(ApplicationUser user, string role) => await _userManager.AddToRoleAsync(user, role);
        public async Task Logout() => await _signInManager.SignOutAsync();
    }
}
