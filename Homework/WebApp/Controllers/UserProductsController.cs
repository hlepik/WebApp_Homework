using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class UserProductsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public UserProductsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: UserProducts
        public async Task<IActionResult> Index()
        {
            var res = await _uow.UserProducts.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: UserProducts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProducts = await _uow.UserProducts.FirstOrDefaultAsync(id.Value);
            if (userProducts == null)
            {
                return NotFound();
            }

            return View(userProducts);
        }

        // GET: UserProducts/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BookingId"] = new SelectList(await _uow.Booking.GetAllAsync(false), "Id", "Id");
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(false), "Id", "FirstName");
            return View();
        }

        // POST: UserProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserProducts userProducts)
        {
            if (ModelState.IsValid)
            {
                _uow.UserProducts.Add(userProducts);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userProducts.UserId);
            return View(userProducts);
        }

        // GET: UserProducts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProducts = await _uow.UserProducts.FirstOrDefaultAsync(id.Value);
            if (userProducts == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userProducts.UserId);
            return View(userProducts);
        }

        // POST: UserProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserProducts userProducts)
        {
            if (id != userProducts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.UserProducts.Update(userProducts);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserProductsExists(userProducts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userProducts.UserId);
            return View(userProducts);
        }

        // GET: UserProducts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProducts = await _uow.UserProducts.FirstOrDefaultAsync(id.Value);
            if (userProducts == null)
            {
                return NotFound();
            }

            return View(userProducts);
        }

        // POST: UserProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.UserProducts.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserProductsExists(Guid id)
        {
            return await _uow.UserProducts.ExistsAsync(id);
        }
    }
}
