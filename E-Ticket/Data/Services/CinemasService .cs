using E_Ticket.Data.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;

namespace E_Ticket.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(DataContext context) : base(context) { }
    }
}
