using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
#pragma warning disable 1591

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IAppBLL _bll;

        public DeletePersonalDataModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<DeletePersonalDataModel> logger, IAppBLL bll)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _bll = bll;

        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "ErrorMessage_Required")]

            [DataType(DataType.Password)]
            public string Password { get; set; } = default!;

        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            var id = await _bll.Product.GetId(User.GetUserId()!.Value);

            foreach (var each in id)
            {
                _bll.ProductMaterial.RemoveProductMaterialsAsync(each!.Id);
                _bll.Picture.RemovePictureAsync(each.Id);
            }

            var bookingId = await _bll.Booking.GetUsersBookings(User.GetUserId()!.Value);
            foreach (var each in bookingId)
            {
                _bll.UserBookedProducts.RemoveUserBookedProductsAsync(each!.ProductId);
                var bookingStatus = await _bll.Product.ChangeBookingStatus(each.ProductId);
                bookingStatus.IsBooked = false;
                _bll.Product.Update(bookingStatus);

            }
            _bll.Booking.RemoveBookingAsync(null, User.GetUserId()!.Value);
            _bll.Product.DeleteAll(User.GetUserId()!.Value);

            _bll.UserMessages.RemoveUserMessagesByUser(User.GetUserId()!.Value);
            var result = await _userManager.DeleteAsync(user);

            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
