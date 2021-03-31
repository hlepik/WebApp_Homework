using System;
using Contracts.Domain.Base;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DTO.App
{
    public class UserMessagesDTO : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        public Guid MessageFormId { get; set; }
        public MessageForm? MessageForm { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}