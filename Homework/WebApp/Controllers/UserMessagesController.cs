using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using UserMessages = BLL.App.DTO.UserMessages;
#pragma warning disable 1591

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
            return View(await _bll.UserMessages.GetAllMessagesAsync(User.GetUserId()!.Value));
        }

        // GET: UserMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _bll.UserMessages.FirstOrDefaultUserMessagesAsync(id.Value, User.GetUserId()!.Value);
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
        public async Task<IActionResult> Create(BLL.App.DTO.UserMessages userMessages)
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

            // var userMessages= await _bll.UserMessages.FirstOrDefaultUserMessagesAsync(id.Value, User.GetUserId()!.Value);
            // if (userMessages == null)
            // {
            //     return NotFound();
            // }
            return View("~/Views/MessageForms/_CreateEdit.cshtml");

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

            var userMessages = await _bll.UserMessages.FirstOrDefaultUserMessagesAsync(id.Value, User.GetUserId()!.Value);

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
            _bll.UserMessages.RemoveUserMessagesAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
