using E_Ticket.Data.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;

namespace E_Ticket.Data.Services
{
    public class ProducersService:EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(DataContext context):base(context) { }
    }
}
