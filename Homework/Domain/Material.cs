using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Material
    {
        public Guid Id { get; set; }

        [MaxLength(256)] public String Name { get; set; } = default!;

        [MaxLength(500)]
        public String? Comment { get; set; }

        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}