using System;
using System.Threading.Tasks;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class UserMessages: DomainEntityId
    {
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
        public DateTime DateSent { get; set; }

        public Guid MessageFormId { get; set; }
        public MessageForm? MessageForm { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string SenderEmail { get; set; } = default!;

    }
}