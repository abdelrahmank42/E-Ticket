using E_Ticket.Controllers;
using E_Ticket.Controllers.Base;
using E_Ticket.Data;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Tickets.Controllers
{
    public class ProducersController :BaseCRUDController<Producer>
    {
        public ProducersController(IProducersService service):base(service) { }
    }
}
