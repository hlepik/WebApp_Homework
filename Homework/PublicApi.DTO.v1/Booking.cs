using System;

namespace PublicApi.DTO.v1
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set;}
        public DateTime TimeBooked { get; set;}
        public DateTime Until { get; set;}
        public Guid AppUserId { get; set; }
    }
}