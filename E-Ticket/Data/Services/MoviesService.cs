using E_Ticket.Data.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Data.ViewModels;
using E_Ticket.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly DataContext _context;
        public MoviesService(DataContext context) : base(context) => _context = context;


        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            Movie newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                Poster = data.Poster,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Category = data.Category,
                ProducerId = data.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //add movie actors
            foreach (int actorId in data.ActorIds)
            {
                Actor_Movie newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public override async Task<Movie> GetByIdAsync(int id) => await _context.Movies
            .Include(c => c.Cinema)
            .Include(p => p.Producer)
            .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
            .FirstOrDefaultAsync(n => n.Id == id);

        public async Task<MovieDropdownsVM> GetMovieDropdownsValues() =>
            new MovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.Name).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.Name).ToListAsync(),
            };

        public async Task UpdateMovieAsync(int id, NewMovieVM data)
        {
            Movie dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == id);
            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.Poster = data.Poster;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.Category = data.Category;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }
            //Remove existing actors
            _context.Actors_Movies.RemoveRange(await _context.Actors_Movies.Where(n => n.MovieId == id).ToListAsync());
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
