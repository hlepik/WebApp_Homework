using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class City : DomainEntityId
    {

        public String Name { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }

    }
}