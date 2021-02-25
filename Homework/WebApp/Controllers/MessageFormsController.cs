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
    public class MessageFormsController : Controller
    {
        private readonly AppDbContext _context;

        public MessageFormsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MessageForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.MessageForms.ToListAsync());
        }

        // GET: MessageForms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageForm = await _context.MessageForms
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,Email,Subject,Message")] MessageForm messageForm)
        {
            if (ModelState.IsValid)
            {
                messageForm.Id = Guid.NewGuid();
                _context.Add(messageForm);
                await _context.SaveChangesAsync();
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

            var messageForm = await _context.MessageForms.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Email,Subject,Message")] MessageForm messageForm)
        {
            if (id != messageForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageFormExists(messageForm.Id))
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

            var messageForm = await _context.MessageForms
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var messageForm = await _context.MessageForms.FindAsync(id);
            _context.MessageForms.Remove(messageForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageFormExists(Guid id)
        {
            return _context.MessageForms.Any(e => e.Id == id);
        }
    }
}
