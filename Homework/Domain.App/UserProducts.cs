using System;
using Domain.Base;

namespace Domain.App
{
    public class UserProducts : DomainEntityId
    {

        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime DateRemoved { get; set; }


        public Guid UserId { get; set;}
        public User? User { get; set; }

        public Guid ProductTypeId { get; set;}
        public Product? Products { get; set; }

    }
}