using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Unit : DomainEntityId
    {

        [MaxLength(54)] public String Name { get; set; } = default!;


        public ICollection<Product>? Products { get; set; }

    }
}