using System;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{

    public class UserBookedProducts : DomainEntityId
    {
        public Guid ProductId { get; set;}
        public Product? Product { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}