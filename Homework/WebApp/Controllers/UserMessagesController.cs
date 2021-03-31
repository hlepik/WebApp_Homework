using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class UserMessagesController : Controller
    {
        private readonly IAppBLL _bll;

        public UserMessagesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: UserMessages
        public async Task<IActionResult> Index()
        {

            return View(await _bll.UserMessages.GetAllMessagesAsync(User.GetUserEmail()));

        }

        // GET: UserMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _bll.UserMessages.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (userMessage== null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // GET: UserMessages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( UserMessages userMessages)
        {
            if (!ModelState.IsValid) return View(userMessages);

            _bll.UserMessages.Add(userMessages);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessages= await _bll.UserMessages.FirstOrDefaultAsync(id.Value);
            if (userMessages == null)
            {
                return NotFound();
            }
            return View(userMessages);
        }

        // POST: UserMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserMessages userMessages)
        {
            if (id != userMessages.Id)
            {
                return NotFound();
            }

            _bll.UserMessages.Update(userMessages);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessages = await _bll.UserMessages.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

            if (userMessages == null)
            {
                return NotFound();
            }

            return View(userMessages);
        }

        // POST: UserMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.UserMessages.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
