using System;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class UserBookings: DomainEntityId
    {

        public Guid BookingId { get; set; }
        public Booking? Booking { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}