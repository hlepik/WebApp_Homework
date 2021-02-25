using System;
using System.Collections.Generic;

namespace Domain
{
    public class Booking
    {
        public Guid Id { get; set; }

        public DateTime TimeBooked { get; set; } = DateTime.Now;
        public DateTime? Until { get; set; }

        public ICollection<UserProducts>? UserProductsCollection { get; set; }


        public ICollection<BookingStatus>? BookingStatus { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }


    }
}