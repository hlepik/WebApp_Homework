using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{

    public class UserBookedProducts : DomainEntityId
    {

        public Guid BookingId { get; set;}
        public Booking? Booking { get; set; }


    }
}