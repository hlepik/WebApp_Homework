using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Booking : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {

        [DataType(DataType.DateTime)]
        public DateTime TimeBooked { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Until { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<UserBookedProducts>? UserBookedProducts { get; set; }
    }
}