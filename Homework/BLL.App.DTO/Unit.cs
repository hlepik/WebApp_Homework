using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Unit : DomainEntityId
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Units), Name = "Name")]
        [MaxLength(54, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(1, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string Name { get; set; } = default!;


        public ICollection<Product>? Products { get; set; }

    }
}