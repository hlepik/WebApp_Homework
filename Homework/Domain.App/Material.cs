using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Material : DomainEntityId
    {

        [MaxLength(256)] public String Name { get; set; } = default!;

        [MaxLength(500)]
        public String? Comment { get; set; }

        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}