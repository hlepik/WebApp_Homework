using System;
using Domain.Base;

namespace Domain.App
{
    public class ProductMaterial : DomainEntityId
    {

        public Guid MaterialId { get; set;}
        public Material? Material { get; set; }

        public Guid ProductId { get; set;}
        public Product? Products { get; set; }


    }
}