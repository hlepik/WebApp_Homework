using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Booking
{
    public class BookingCreateEditViewModels
    {
        public Domain.App.Booking Booking { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
    }
}