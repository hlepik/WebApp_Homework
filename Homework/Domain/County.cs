using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Domain
{
    public class County
    {
        public Guid Id { get; set; }

        [MaxLength(128)] public String Name { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }


    }
}