using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Material = BLL.App.DTO.Material;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MaterialsController : Controller
    {

        private readonly IAppBLL _bll;

        public MaterialsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Materials

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Material.GetAllAsync());
        }

        [AllowAnonymous]
        // GET: Materials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var contactType = await _bll.Material
                .FirstOrDefaultAsync(id.Value);

            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }



        // GET: Materials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Material material)
        {
            if (!ModelState.IsValid) return View(material);

            _bll.Material.Add(material);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _bll.Material.FirstOrDefaultAsync(id.Value);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Material material)
        {
            if (id != material.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return View(material);

            _bll.Material.Update(material);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _bll.Material.FirstOrDefaultAsync(id.Value);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Material.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

    }
}
