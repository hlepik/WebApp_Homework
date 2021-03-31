using System;
using System.Collections.Generic;
using DAL.App.DTO;
using Domain.Base;

namespace DTO.App
{
    public class BookingDTO : DomainEntityId
    {
        public DateTime TimeBooked { get; set; } = DateTime.Now;
        public DateTime? Until { get; set; }



        public Guid ProductId { get; set; }
        public ProductDTO? Product { get; set; }

        public Guid UserBookingId { get; set; }
        public UserBookings? UserBookings { get; set; }

    }

}