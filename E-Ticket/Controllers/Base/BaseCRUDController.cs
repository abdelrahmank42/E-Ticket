using E_Ticket.Data.Base;
using E_Ticket.Data.Services.IServices;
using E_Ticket.Models;
using eTicket.Data.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace E_Ticket.Controllers.Base
{
    [Authorize(Roles = UserRoles.Admin)]
    public class BaseCRUDController<T> : Controller, IBaseCRUDController<T> where T : class, IEntityBase, new()
    {
        private readonly IEntityBaseRepository<T> _service;

        public BaseCRUDController(IEntityBaseRepository<T> entity) => _service = entity;

        public IActionResult Create() => View();
        [HttpPost]
        public virtual async Task<IActionResult> Create(T entity)
        {
            if (!ModelState.IsValid)
                return View(entity);
            await _service.AddAsync(entity);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public virtual async Task<IActionResult> Index() => View(await _service.GetAllAsync());
        [AllowAnonymous]
        public virtual async Task<IActionResult> Details(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null) return View("NotFound");
            return View(entity);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            return entity != null ? View(entity) : View("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, T entity)
        {
            if (!ModelState.IsValid)
                return View(entity);
            await _service.UpdateAsync(entity);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            return entity != null ? View(entity) : View("NotFound");
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
