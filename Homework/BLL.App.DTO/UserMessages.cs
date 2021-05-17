using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class UserMessages: DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.UserMessage), Name = "Email")]
        public string? Email { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.UserMessage), Name = "Message")]
        public string? Message { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.UserMessage), Name = "Subject")]
        public string? Subject { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.UserMessage), Name = "DateSent")]
        public DateTime DateSent { get; set; }

        public Guid AppUserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.UserMessage), Name = "SenderEmail")]
        public string SenderEmail { get; set; } = default!;

    }
}