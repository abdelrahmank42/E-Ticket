using E_Ticket.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Ticket.Data.Services.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> AddRoleAsync(ApplicationUser user, string role);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string pass);
        Task<IdentityResult> CreateNewUserAsync(ApplicationUser newUser, string pass);
        Task<ApplicationUser> FindByEmailAsync(string Email);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task Logout();
        Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string pass);
    }
}