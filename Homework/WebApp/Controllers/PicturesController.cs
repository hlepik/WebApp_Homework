using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class PicturesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PicturesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Pictures
        public async Task<IActionResult> Index()
        {

            var res = await _uow.Picture.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _uow.Picture.FirstOrDefaultAsync(id.Value);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // GET: Pictures/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductId"] = new SelectList(await _uow.Product.GetAllAsync(false), "Id", "Description");
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Picture picture)
        {
            if (ModelState.IsValid)
            {
                _uow.Picture.Add(picture);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(await _uow.Product.GetAllAsync(), "Id", "Description", picture.ProductId);
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _uow.Picture.FirstOrDefaultAsync(id.Value);
            if (picture == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(await _uow.Product.GetAllAsync(), "Id", "Description", picture.ProductId);
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  Picture picture)
        {
            if (id != picture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Picture.Update(picture);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PictureExists(picture.Id))
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
            ViewData["ProductId"] = new SelectList(await _uow.Product.GetAllAsync(), "Id", "Description", picture.ProductId);
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _uow.Picture.FirstOrDefaultAsync(id.Value);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Picture.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PictureExists(Guid id)
        {
            return await _uow.Picture.ExistsAsync(id);
        }
    }
}
