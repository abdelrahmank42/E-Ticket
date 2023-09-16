using E_Ticket.Controllers.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tickets.Controllers
{
    public class MoviesController : BaseCRUDController<Movie>
    {
        private readonly IMoviesService _moviesService;
        public MoviesController(IMoviesService service) : base(service) => _moviesService = service;

        public override async Task<IActionResult> Index() => View(await _moviesService.GetAllAsync(m => m.Cinema));
        public override async Task<IActionResult> Details(int id)
        {
            Movie movie = await _moviesService.GetByIdAsync(id, m=>m.Cinema, m=>m.Actor_Movies, m=>m.Producer);

            if (movie == null) return View("NotFound");
            return View(movie);
        }
    }
}
