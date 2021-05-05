using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Condition : DomainEntityId
    {


        public Guid DescriptionId { get; set; }
        [MaxLength(1000)] public LangString? Description { get; set; }
        // [MaxLength(1000)] public string Description { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }
    }
}