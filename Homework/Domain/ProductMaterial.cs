using System;
using System.Collections.Generic;

namespace Domain
{
    public class ProductMaterial
    {
        public Guid Id { get; set; }

        public Guid MaterialId { get; set;}
        public Material? Material { get; set; }

        public Guid ProductId { get; set;}
        public Product? Products { get; set; }


    }
}