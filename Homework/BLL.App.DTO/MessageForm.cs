using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class MessageForm : DomainEntityId
    {

        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.MessageForms), Name = "Email")]
        [MaxLength(64, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.MessageForms), Name = "Subject")]
        [MaxLength(128, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string Subject { get; set; } = default!;

        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.MessageForms), Name = "Message")]
        [MaxLength(1000, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string Message { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.MessageForms), Name = "DateSent")]
        [DataType(DataType.Date)]
        public DateTime DateSent { get; set; } = DateTime.Now;

        public Guid? SenderId { get; set; }

        public ICollection<UserMessages>? UserMessages { get; set; }



    }
}