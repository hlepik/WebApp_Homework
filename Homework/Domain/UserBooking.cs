using System;

namespace Domain
{
    public class UserBooking
    {
        public Guid Id { get; set; }

        public DateTime DateBooked { get; set; } = DateTime.Now;
        public DateTime DateRemoved { get; set; }

        public Guid BookingId { get; set;}
        public Booking? Booking { get; set; }

        public Guid UserId { get; set;}
        public User? User { get; set; }

    }
}