using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{

    public class BookingStatus : DomainEntityId
    {


        [MaxLength(128)] public String Description { get; set; } = default!;
        [MaxLength(1000)]
        public String? Comment { get; set; }

        public Guid BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}