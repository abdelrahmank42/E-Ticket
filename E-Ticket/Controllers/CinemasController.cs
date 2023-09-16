using E_Ticket.Controllers;
using E_Ticket.Controllers.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Tickets.Controllers
{
    public class CinemasController : BaseCRUDController<Cinema>
    {
        public CinemasController(ICinemasService service) : base(service) { }
    }
}
