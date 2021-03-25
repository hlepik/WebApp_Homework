using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.UserBookedProducts
{
    public class UserBookedProductsCreateEditViewModel
    {
        public Domain.App.UserBookedProducts UserBookedProducts { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
    }
}