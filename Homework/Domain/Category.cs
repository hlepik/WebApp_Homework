using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        [MaxLength(128)] public String Name { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }
    }
}