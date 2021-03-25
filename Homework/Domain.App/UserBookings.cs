using System;
using System.Collections.Generic;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserBookings: DomainEntityId
    {

        public Guid BookingId { get; set; }
        public Booking? Booking { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}