using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class City : DomainEntityId
    {

        public Guid NameId { get; set; }

        [MaxLength(128)] public LangString Name { get; set; } = default!;
        // [MaxLength(128)] public string Name { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }

    }
}