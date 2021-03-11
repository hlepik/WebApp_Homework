using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class MessageFormsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public MessageFormsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: MessageForms
        public async Task<IActionResult> Index()
        {
            var res = await _uow.MessageForm.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: MessageForms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageForm = await _uow.MessageForm.FirstOrDefaultAsync(id.Value);
            if (messageForm == null)
            {
                return NotFound();
            }

            return View(messageForm);
        }

        // GET: MessageForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageForm messageForm)
        {
            if (ModelState.IsValid)
            {
                _uow.MessageForm.Add(messageForm);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messageForm);
        }

        // GET: MessageForms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageForm = await _uow.MessageForm.FirstOrDefaultAsync(id.Value);
            if (messageForm == null)
            {
                return NotFound();
            }
            return View(messageForm);
        }

        // POST: MessageForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MessageForm messageForm)
        {
            if (id != messageForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.MessageForm.Update(messageForm);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MessageFormExists(messageForm.Id))
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
            return View(messageForm);
        }

        // GET: MessageForms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageForm = await _uow.MessageForm.FirstOrDefaultAsync(id.Value);
            if (messageForm == null)
            {
                return NotFound();
            }

            return View(messageForm);
        }

        // POST: MessageForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.MessageForm.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MessageFormExists(Guid id)
        {
            return await _uow.MessageForm.ExistsAsync(id);
        }
    }
}
