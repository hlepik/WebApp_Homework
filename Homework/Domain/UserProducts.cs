using System;
using System.Collections.Generic;

namespace Domain
{
    public class UserProducts
    {
        public Guid Id { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime DateRemoved { get; set; }


        public Guid UserId { get; set;}
        public User? User { get; set; }

        public Guid ProductTypeId { get; set;}
        public Product? Products { get; set; }

    }
}