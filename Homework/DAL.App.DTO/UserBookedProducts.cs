using System;
using Domain.App.Identity;
using Domain.Base;

namespace DAL.App.DTO
{

    public class UserBookedProducts : DomainEntityId
    {

        public DateTime? Until { get; set; }
        public DateTime TimeBooked { get; set; }

        public string Description { get; set; }= default!;
        public string Email { get; set; }= default!;
        public Guid ProductId { get; set;}
        public Product? Product { get; set; }
        public Guid AppUserId { get; set;}


    }
}