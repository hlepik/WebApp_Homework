using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class UserBookedProducts
    {
        public Guid Id { get; set; }
        public DateTime? Until { get; set; }
        public DateTime TimeBooked { get; set; }
        public Guid ProductId { get; set; }
        public Guid AppUserId { get; set; }
        [MaxLength(500)] public string Description { get; set; }= default!;
        [MaxLength(64)] public string Email { get; set; }= default!;
        public Guid BookingId { get; set;}
    }
}