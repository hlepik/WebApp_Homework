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
    public class UserMessagesController : Controller
    {
        private readonly AppDbContext _context;

        public UserMessagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserMessages
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserMessages.Include(u => u.MessageForm).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessages
                .Include(u => u.MessageForm)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // GET: UserMessages/Create
        public IActionResult Create()
        {
            ViewData["MessageFormId"] = new SelectList(_context.MessageForms, "Id", "Email");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName");
            return View();
        }

        // POST: UserMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateSent,UserId,MessageFormId")] UserMessage userMessage)
        {
            if (ModelState.IsValid)
            {
                userMessage.Id = Guid.NewGuid();
                _context.Add(userMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MessageFormId"] = new SelectList(_context.MessageForms, "Id", "Email", userMessage.MessageFormId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userMessage.UserId);
            return View(userMessage);
        }

        // GET: UserMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessages.FindAsync(id);
            if (userMessage == null)
            {
                return NotFound();
            }
            ViewData["MessageFormId"] = new SelectList(_context.MessageForms, "Id", "Email", userMessage.MessageFormId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userMessage.UserId);
            return View(userMessage);
        }

        // POST: UserMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateSent,UserId,MessageFormId")] UserMessage userMessage)
        {
            if (id != userMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMessageExists(userMessage.Id))
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
            ViewData["MessageFormId"] = new SelectList(_context.MessageForms, "Id", "Email", userMessage.MessageFormId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userMessage.UserId);
            return View(userMessage);
        }

        // GET: UserMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessages
                .Include(u => u.MessageForm)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var userMessage = await _context.UserMessages.FindAsync(id);
            _context.UserMessages.Remove(userMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMessageExists(Guid id)
        {
            return _context.UserMessages.Any(e => e.Id == id);
        }
    }
}
