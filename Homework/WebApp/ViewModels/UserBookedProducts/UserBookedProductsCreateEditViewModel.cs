using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.UserBookedProducts
{
    public class UserBookedProductsCreateEditViewModel
    {
        public BLL.App.DTO.UserBookedProducts UserBookedProducts { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
    }
}