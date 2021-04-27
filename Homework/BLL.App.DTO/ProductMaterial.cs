using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ProductMaterial : DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Product")]
        public string? Product { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Materials), Name = "Material")]
        public string? Material { get; set; }
        public Guid MaterialId { get; set;}
        public Guid ProductId { get; set;}



    }
}