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

            var res = await _uow.UserMessage.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: UserMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _uow.UserMessage.FirstOrDefaultAsync(id.Value);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // GET: UserMessages/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MessageFormId"] = new SelectList(await _uow.MessageForm.GetAllAsync(false), "Id", "Email");
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(false), "Id", "FirstName");
            return View();
        }

        // POST: UserMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserMessage userMessage)
        {
            if (ModelState.IsValid)
            {
                _uow.UserMessage.Add(userMessage);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MessageFormId"] = new SelectList(await _uow.MessageForm.GetAllAsync(), "Id", "Email", userMessage.MessageFormId);
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userMessage.UserId);
            return View(userMessage);
        }

        // GET: UserMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _uow.UserMessage.FirstOrDefaultAsync(id.Value);
            if (userMessage == null)
            {
                return NotFound();
            }
            ViewData["MessageFormId"] = new SelectList(await _uow.MessageForm.GetAllAsync(), "Id", "Email", userMessage.MessageFormId);
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userMessage.UserId);
            return View(userMessage);
        }

        // POST: UserMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserMessage userMessage)
        {
            if (id != userMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.UserMessage.Update(userMessage);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserMessageExists(userMessage.Id))
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
            ViewData["MessageFormId"] = new SelectList(await _uow.MessageForm.GetAllAsync(), "Id", "Email", userMessage.MessageFormId);
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userMessage.UserId);
            return View(userMessage);
        }

        // GET: UserMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _uow.UserMessage.FirstOrDefaultAsync(id.Value);

            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // POST: UserMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.UserMessage.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserMessageExists(Guid id)
        {
            return await _uow.UserMessage.ExistsAsync(id);
        }
    }
}
