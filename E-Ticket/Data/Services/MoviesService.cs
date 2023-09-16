using E_Ticket.Data.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Ticket.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly DataContext _context;
        public MoviesService(DataContext context) : base(context) => _context = context;
        public override async Task<Movie> GetByIdAsync(int id)
        {
            Movie movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }
    }
}
