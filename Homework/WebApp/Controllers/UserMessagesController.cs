using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IAppUnitOfWork _uow;

        public UserMessagesController(IAppUnitOfWork uow)
        {

            _uow = uow;
        }

        // GET: UserMessages
        public async Task<IActionResult> Index()
        {

            var res = await _uow.MessageForm.GetAllMessagesAsync(User.GetUserEmail());
            return View(res);
        }

        // GET: UserMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _uow.UserMessages.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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

            _uow.UserMessages.Add(userMessages);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessages= await _uow.UserMessages.FirstOrDefaultAsync(id.Value);
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

            _uow.UserMessages.Update(userMessages);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessages = await _uow.UserMessages.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

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
            await _uow.UserMessages.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
