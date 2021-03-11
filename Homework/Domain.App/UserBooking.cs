using System;
using Domain.Base;

namespace Domain.App
{
    public class UserBooking : DomainEntityId
    {

        public DateTime DateBooked { get; set; } = DateTime.Now;
        public DateTime DateRemoved { get; set; }

        public Guid BookingId { get; set;}
        public Booking? Booking { get; set; }

        public Guid UserId { get; set;}
        public User? User { get; set; }

    }
}