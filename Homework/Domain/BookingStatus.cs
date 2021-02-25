using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class BookingStatus
    {
        public Guid Id { get; set; }

        [MaxLength(128)] public String Description { get; set; } = default!;
        [MaxLength(1000)]
        public String? Comment { get; set; }

        public Guid BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}