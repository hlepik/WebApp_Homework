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
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
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
            var res = await _uow.MessageForm.GetAllAsync(User.GetUserId()!.Value);
            return View(res);
        }

        // GET: MessageForms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageForm = await _uow.MessageForm.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (messageForm == null)
            {
                return NotFound();
            }

            return View(messageForm);
        }

        // GET: MessageForms/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: MessageForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MessageForm messageForm)
        {
            if (ModelState.IsValid)
            {


                _uow.MessageForm.Add(messageForm);
                messageForm.SenderId = User.GetUserId()!.Value;

                await _uow.SaveChangesAsync();
                    var userMessage = new UserMessages
                {
                    MessageFormId = messageForm.Id,
                    UserId = User.GetUserId()!.Value,

                };

                _uow.UserMessages.Add(userMessage);
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

            var messageForm = await _uow.MessageForm.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
                _uow.MessageForm.Update(messageForm);
                await _uow.SaveChangesAsync();
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

            var messageForm = await _uow.MessageForm.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

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


    }
}
