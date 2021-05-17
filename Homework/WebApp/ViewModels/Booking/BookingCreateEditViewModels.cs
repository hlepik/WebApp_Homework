using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable 1591

namespace WebApp.ViewModels.Booking
{
    public class BookingCreateEditViewModels
    {
        public BLL.App.DTO.Booking Booking { get; set; } = default!;

        public BLL.App.DTO.Product Products { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
    }
}