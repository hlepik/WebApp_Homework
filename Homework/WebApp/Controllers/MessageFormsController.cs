using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using MessageForm = BLL.App.DTO.MessageForm;
using UserMessages = BLL.App.DTO.UserMessages;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize]
    public class MessageFormsController : Controller
    {
        private readonly IAppBLL _bll;


        public MessageFormsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: MessageForms
        public async Task<IActionResult> Index()
        {
            return View(await _bll.MessageForm.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: MessageForms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var messageForm = await _bll.MessageForm.FirstOrDefaultMessagesAsync(id.Value);
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
                messageForm.DateSent = DateTime.Now;
                messageForm.SenderId = User.GetUserId()!.Value;

                var id = await _bll.UserMessages.GetId(messageForm.Email);

                if (id == Guid.Empty)
                {
                    ModelState.AddModelError("NoEmail", "Email is not correct!");

                    return View();
                }
                _bll.MessageForm.Add(messageForm);
                await _bll.SaveChangesAsync();


                var userMessage = new UserMessages
                {
                    AppUserId =  id!.Value,
                    SenderEmail = User.GetUserEmail(),
                    Subject = messageForm.Subject,
                    Message = messageForm.Message,
                    DateSent = messageForm.DateSent

                };

                _bll.UserMessages.Add(userMessage);
                await _bll.SaveChangesAsync();

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

            var messageForm = await _bll.MessageForm.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
                _bll.MessageForm.Update(messageForm);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(messageForm);
        }


        // POST: MessageForms/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            _bll.MessageForm.RemoveMessagesAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
