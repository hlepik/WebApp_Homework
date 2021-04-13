using System;
using System.Collections;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserMessages: DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {

        public Guid? MessageFormId { get; set; }
        public MessageForm? MessageForm { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public string SenderEmail { get; set; } = default!;


    }
}