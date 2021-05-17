using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ProductMaterial : DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Product")]
        public string? ProductName { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Materials), Name = "Material")]
        public string? MaterialName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Materials), Name = "Material")]
        public Guid MaterialId { get; set;}
        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Product")]
        public Guid ProductId { get; set;}



    }
}