using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Areas.Admin.ViewModels;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class RolesController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;


        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        // GET: Admin/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        // GET: Admin/Roles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // GET: Admin/Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                appRole.Id = Guid.NewGuid();
                _context.Roles.Update(appRole);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(appRole);
        }

        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            if (appRole == null)
            {
                return NotFound();
            }
            return View(appRole);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Roles.Update(appRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppRoleExists(appRole.Id))
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
            return View(appRole);
        }

        // GET: Admin/Roles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(appRole);

            return RedirectToAction(nameof(Index));
        }

        //Get: Admin/Roles/AddRole
        public async Task<IActionResult> AddRole(Guid id)
        {

            var role = await _roleManager.FindByIdAsync(id.ToString());
            var vm = new AppUserAddRoleViewModel();
            vm.Role = role;


            vm.Members = new List<AppUser>();
            vm.NonMembers = new List<AppUser>();
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    vm.Members.Add(user);
                }
                else
                {
                    vm.NonMembers.Add(user);
                }
            }

            return View(vm);
        }

        [HttpPost, ActionName("AddRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleConfirmed(AppUserAddRoleViewModel vm, Guid id)
        {


            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            IdentityResult result = new IdentityResult();
            if (vm.AddIds != null)
            {
                foreach (var each in vm.AddIds)
                {
                    var user = await _userManager.FindByIdAsync(each.ToString());

                    result = await _userManager.AddToRoleAsync(user, appRole.Name);
                }
                if (result!.Succeeded)
                    return RedirectToAction(nameof(Index));

            }

            vm.Members = new List<AppUser>();
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, appRole.Name))
                {
                    vm.Members.Add(user);
                }

            }
            var count = vm.Members.Count;

            if (vm.DeleteIds != null)
            {

                foreach (var each in vm.DeleteIds)
                {
                    var user = await _userManager.FindByIdAsync(each.ToString());

                    if (count > 1)
                    {
                        count--;
                        result = await _userManager.RemoveFromRoleAsync(user, appRole.Name);
                    }
                    else
                    {
                        if (result!.Succeeded)
                            return RedirectToAction(nameof(Index));
                    }

                }
                if (result!.Succeeded)
                    return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }



        private bool AppRoleExists(Guid id)
        {
            return _roleManager.Roles.Any(e => e.Id == id);
        }
    }
}
