using E_Ticket.Data.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;

namespace E_Ticket.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(DataContext context) : base(context) { }
    }
}
