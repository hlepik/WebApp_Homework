using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class ProductPicturesController : Controller
    {
        private readonly AppDbContext _context;

        public ProductPicturesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductPictures
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductPictures.Include(p => p.Picture).Include(p => p.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductPictures/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPictures = await _context.ProductPictures
                .Include(p => p.Picture)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPictures == null)
            {
                return NotFound();
            }

            return View(productPictures);
        }

        // GET: ProductPictures/Create
        public IActionResult Create()
        {
            ViewData["PictureId"] = new SelectList(_context.Pictures, "Id", "Url");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: ProductPictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PictureId,ProductId")] ProductPictures productPictures)
        {
            if (ModelState.IsValid)
            {
                productPictures.Id = Guid.NewGuid();
                _context.Add(productPictures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PictureId"] = new SelectList(_context.Pictures, "Id", "Url", productPictures.PictureId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productPictures.ProductId);
            return View(productPictures);
        }

        // GET: ProductPictures/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPictures = await _context.ProductPictures.FindAsync(id);
            if (productPictures == null)
            {
                return NotFound();
            }
            ViewData["PictureId"] = new SelectList(_context.Pictures, "Id", "Url", productPictures.PictureId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productPictures.ProductId);
            return View(productPictures);
        }

        // POST: ProductPictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PictureId,ProductId")] ProductPictures productPictures)
        {
            if (id != productPictures.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPictures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPicturesExists(productPictures.Id))
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
            ViewData["PictureId"] = new SelectList(_context.Pictures, "Id", "Url", productPictures.PictureId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productPictures.ProductId);
            return View(productPictures);
        }

        // GET: ProductPictures/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPictures = await _context.ProductPictures
                .Include(p => p.Picture)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPictures == null)
            {
                return NotFound();
            }

            return View(productPictures);
        }

        // POST: ProductPictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productPictures = await _context.ProductPictures.FindAsync(id);
            _context.ProductPictures.Remove(productPictures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPicturesExists(Guid id)
        {
            return _context.ProductPictures.Any(e => e.Id == id);
        }
    }
}
