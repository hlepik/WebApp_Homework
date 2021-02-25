using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(64)] public String FirstName { get; set; } = default!;

        [MaxLength(64)] public String LastName { get; set; } = default!;

        [MaxLength(32)] public String Password { get; set; } = default!;

        [MaxLength(32)] public String UserName { get; set; } = default!;

        public ICollection<UserMessage>? UserMessages { get; set; }
        public ICollection<UserBooking>? UserBookings { get; set; }
        public ICollection<UserProducts>? UserProductsCollection { get; set; }
    }
}