using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Condition : DomainEntityId
    {
        public Guid DescriptionId { get; set; }

        [MaxLength(1000)] public string Description { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }

    }
}