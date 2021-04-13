using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable 1591

namespace WebApp.ViewModels.UserBookedProducts
{
    public class UserBookedProductsCreateEditViewModel
    {
        public BLL.App.DTO.UserBookedProducts UserBookedProducts { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
    }
}