using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class Booking : DomainEntityId
    {

        public DateTime TimeBooked { get; set; } = DateTime.Now;
        public DateTime? Until { get; set; }

        public ICollection<UserProducts>? UserProductsCollection { get; set; }


        public ICollection<BookingStatus>? BookingStatus { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }


    }
}