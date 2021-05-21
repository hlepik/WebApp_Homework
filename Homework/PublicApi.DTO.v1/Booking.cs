using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set;}
        public DateTime TimeBooked { get; set;}
        [DataType(DataType.Date)]
        public DateTime? Until { get; set;}
        public Guid AppUserId { get; set; }
    }
}