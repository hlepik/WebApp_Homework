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
    public class ProductMaterialsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductMaterialsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductMaterials
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductMaterials.Include(p => p.Material).Include(p => p.Products);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductMaterials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMaterial = await _context.ProductMaterials
                .Include(p => p.Material)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productMaterial == null)
            {
                return NotFound();
            }

            return View(productMaterial);
        }

        // GET: ProductMaterials/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: ProductMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaterialId,ProductId")] ProductMaterial productMaterial)
        {
            if (ModelState.IsValid)
            {
                productMaterial.Id = Guid.NewGuid();
                _context.Add(productMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", productMaterial.MaterialId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productMaterial.ProductId);
            return View(productMaterial);
        }

        // GET: ProductMaterials/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMaterial = await _context.ProductMaterials.FindAsync(id);
            if (productMaterial == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", productMaterial.MaterialId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productMaterial.ProductId);
            return View(productMaterial);
        }

        // POST: ProductMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MaterialId,ProductId")] ProductMaterial productMaterial)
        {
            if (id != productMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductMaterialExists(productMaterial.Id))
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
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", productMaterial.MaterialId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productMaterial.ProductId);
            return View(productMaterial);
        }

        // GET: ProductMaterials/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMaterial = await _context.ProductMaterials
                .Include(p => p.Material)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productMaterial == null)
            {
                return NotFound();
            }

            return View(productMaterial);
        }

        // POST: ProductMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productMaterial = await _context.ProductMaterials.FindAsync(id);
            _context.ProductMaterials.Remove(productMaterial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductMaterialExists(Guid id)
        {
            return _context.ProductMaterials.Any(e => e.Id == id);
        }
    }
}
