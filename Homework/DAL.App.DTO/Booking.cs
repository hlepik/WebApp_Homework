using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Booking : DomainEntityId
    {

        public DateTime TimeBooked { get; set; } = DateTime.Now;
        public DateTime? Until { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid UserBookingId { get; set; }
        public UserBookings? UserBookings { get; set; }

    }
}