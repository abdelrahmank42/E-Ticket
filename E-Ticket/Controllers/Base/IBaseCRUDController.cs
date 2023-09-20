using E_Ticket.Data.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Ticket.Controllers.Base
{
    public interface IBaseCRUDController<T> where T : class, IEntityBase, new()
    {
        public Task<IActionResult> Index();


        public Task<IActionResult> Create();
        public Task<IActionResult> Create(T entity);


        public Task<IActionResult> Details(int id);


        public Task<IActionResult> Edit(int id);
        public Task<IActionResult> Edit(int id, T entity);


        public Task<IActionResult> Delete(int id);
        public Task<IActionResult> DeleteConfirm(int id);
    }

}
