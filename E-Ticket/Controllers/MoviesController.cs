using E_Ticket.Controllers.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Data.ViewModels;
using E_Ticket.Models;
using eTicket.Data.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : BaseCRUDController<Movie>
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service):base(service)=> _service = service;

        [AllowAnonymous]
        public override async Task<IActionResult> Index() => View(await _service.GetAllAsync(n=>n.Cinema));

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            IEnumerable<Movie> allMovies = await _service.GetAllAsync();
            if(!string.IsNullOrEmpty(searchString))
            {
                IEnumerable<Movie> filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
                return View("Index", filteredResultNew);
            }
            return View("Index", allMovies);
        }

        public override async Task<IActionResult> Create()
        {
            MovieDropdownsVM movieDropdowns = await _service.GetMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdowns.Producers, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdowns.Actors, "Id", "Name");
            
            return View();
        }
    }
}
