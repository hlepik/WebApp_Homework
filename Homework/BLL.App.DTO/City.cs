using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class City : DomainEntityId
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Cities), Name = "Name")]
        [MaxLength(128, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string Name { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }
        public Guid NameId { get; set; }

    }
}