using System;
using Domain.App.Identity;
using Domain.Base;

namespace DAL.App.DTO
{

    public class UserBookedProducts : DomainEntityId
    {

        public DateTime? Until { get; set; }
        public DateTime TimeBooked { get; set; }
        public bool HasTransport { get; set; }
        public string? County { get; set; }
        public string? City { get; set; }
        public string? LocationDescription { get; set; }
        public string? Color { get; set; }
        public Guid AppUserId { get; set; }
        public string Description { get; set; }= default!;
        public string Email { get; set; }= default!;
        public Guid BookingId { get; set;}
        public Booking? Booking { get; set;}


    }
}