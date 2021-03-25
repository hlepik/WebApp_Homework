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
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Picture;

namespace WebApp.Controllers
{
    [Authorize]
    public class PicturesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PicturesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Pictures
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Picture.GetAllAsync(User.GetUserId()!.Value);
            return View(res);
        }

        [AllowAnonymous]
        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _uow.Picture
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // GET: Pictures/Create
        public async Task<IActionResult> Create()
        {
            // ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            var vm = new PictureCreateEditViewModels();
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
                nameof(Product.Description));
            return View(vm);
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PictureCreateEditViewModels vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Picture.Add(vm.Picture);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", picture.ProductId);
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.Picture.ProductId);
            return View(vm);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _uow.Picture.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (picture == null)
            {
                return NotFound();
            }
            var vm = new PictureCreateEditViewModels();
            vm.Picture = picture;
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.Picture.ProductId);
            return View(vm);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PictureCreateEditViewModels vm)
        {
            if (id != vm.Picture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Picture.Update(vm.Picture);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.Picture.ProductId);
            return View(vm);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _uow.Picture.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);;
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

    }
}
