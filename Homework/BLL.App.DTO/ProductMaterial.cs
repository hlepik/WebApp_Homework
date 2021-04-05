using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ProductMaterial : DomainEntityId
    {

        public string? ProductName { get; set; }
        public string? MaterialName { get; set; }
        public Guid ProductOwner { get; set;}
        public Guid MaterialId { get; set;}
        public Material? Material { get; set; }

        public Guid ProductId { get; set;}
        public Product? Products { get; set; }


    }
}