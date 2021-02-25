using System;
using System.Collections.Generic;

namespace Domain
{
    public class City
    {
        public Guid Id { get; set; }

        public String Name { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }

    }
}