using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Unit : DomainEntityId
    {
        public Guid NameId { get; set; }

        [MaxLength(54)] public string Name { get; set; } = default!;


        public ICollection<Product>? Products { get; set; }

    }
}