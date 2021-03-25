using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Material : DomainEntityId
    {

        [MaxLength(256)] public string Name { get; set; } = default!;

        [MaxLength(500)]public string? Comment { get; set; }

        public ICollection<ProductMaterial>? ProductMaterials { get; set; }

    }
}