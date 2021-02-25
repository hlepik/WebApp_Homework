using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Unit
    {
        public Guid Id { get; set; }

        [MaxLength(54)] public String Name { get; set; } = default!;


        public ICollection<Product>? Products { get; set; }

    }
}