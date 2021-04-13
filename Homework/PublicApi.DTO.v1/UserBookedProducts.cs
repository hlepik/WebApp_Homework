using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class UserBookedProducts
    {
        public Guid Id { get; set; }
        public DateTime? Until { get; set; }
        public DateTime TimeBooked { get; set; }
        public bool HasTransport { get; set; }
        [MaxLength(128)] public string? County { get; set; }
        [MaxLength(128)] public string? City { get; set; }
        [MaxLength(500)] public string? LocationDescription { get; set; }
        [MaxLength(64)] public string? Color { get; set; }
        public Guid AppUserId { get; set; }
        [MaxLength(500)] public string Description { get; set; }= default!;
        [MaxLength(64)] public string Email { get; set; }= default!;
        public Guid BookingId { get; set;}
    }
}