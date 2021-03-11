using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Condition : DomainEntityId
    {


        [MaxLength(1000)] public String Description { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }
    }
}