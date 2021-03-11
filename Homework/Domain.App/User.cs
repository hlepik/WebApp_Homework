using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class User : DomainEntityId
    {

        [MaxLength(64)] public String FirstName { get; set; } = default!;

        [MaxLength(64)] public String LastName { get; set; } = default!;

        [MaxLength(32)] public String Password { get; set; } = default!;

        [MaxLength(32)] public String UserName { get; set; } = default!;

        public ICollection<UserMessage>? UserMessages { get; set; }
        public ICollection<UserBooking>? UserBookings { get; set; }
        public ICollection<UserProducts>? UserProductsCollection { get; set; }
    }
}