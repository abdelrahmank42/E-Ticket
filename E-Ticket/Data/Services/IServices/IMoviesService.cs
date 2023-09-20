using E_Ticket.Data.Base;
using E_Ticket.Data.ViewModels;
using E_Ticket.Models;
using System.Threading.Tasks;

namespace E_Ticket.Data.Services.IServices
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<MovieDropdownsVM> GetMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(int id, NewMovieVM data);
    }
}
