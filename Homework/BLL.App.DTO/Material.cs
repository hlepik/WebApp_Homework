using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Material : DomainEntityId
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Materials), Name = "Name")]
        [MaxLength(256, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
         public string Name { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Materials), Name = "Comment")]
        [MaxLength(500, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string? Comment { get; set; }
        public Guid NameId { get; set; }



    }
}