using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Booking
{
    public class BookingCreateEditViewModels
    {
        public BLL.App.DTO.Booking Booking { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
    }
}