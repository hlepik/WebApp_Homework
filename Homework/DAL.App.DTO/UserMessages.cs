using System;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class UserMessages: DomainEntityId
    {

        public Guid MessageFormId { get; set; }
        public MessageForm? MessageForm { get; set; }

        public Guid UserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}