using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{

    public class UserBookedProducts : DomainEntityId
    {
        public Guid ProductId { get; set;}
        public Product? Product { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}