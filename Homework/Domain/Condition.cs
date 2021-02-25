using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Condition
    {
        public Guid Id { get; set; }

        [MaxLength(1000)] public String Description { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }
    }
}