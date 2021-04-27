using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Condition : DomainEntityId
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Conditions), Name = "Description")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string Description { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }
    }
}