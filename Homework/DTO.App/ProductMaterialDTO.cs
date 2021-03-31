using System;
using DAL.App.DTO;
using Domain.Base;

namespace DTO.App
{
    public class ProductMaterialDTO : DomainEntityId
    {
        public Guid MaterialId { get; set;}
        public Material? Material { get; set; }

        public Guid ProductId { get; set;}
        public ProductDTO? Products { get; set; }


    }
}