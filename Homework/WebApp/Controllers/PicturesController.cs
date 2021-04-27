using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Picture;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize]
    public class PicturesController : Controller
    {
        private readonly IAppBLL _bll;

        public PicturesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Pictures

        public async Task<IActionResult> Index()
        {
            return View(await _bll.Picture.GetAllPicturesAsync(User.GetUserId()!.Value));

        }

        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _bll.Picture
                .FirstOrDefaultDTOAsync(id.Value, User.GetUserId()!.Value);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // GET: Pictures/Create
        public async Task<IActionResult> Create()
        {
            var vm = new PictureCreateEditViewModels();
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
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
                _bll.Picture.Add(vm.Picture);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
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

            var picture = await _bll.Picture.FirstOrDefaultDTOAsync(id.Value, User.GetUserId()!.Value);
            if (picture == null)
            {
                return NotFound();
            }
            var vm = new PictureCreateEditViewModels();
            vm.Picture = picture;
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
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
                _bll.Picture.Update(vm.Picture);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
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

            var picture = await _bll.Picture.FirstOrDefaultDTOAsync(id.Value, User.GetUserId()!.Value);
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
            await _bll.Picture.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
