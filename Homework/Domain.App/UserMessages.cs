using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App.Identity
{
    public class UserMessages: DomainEntityId
    {

        public Guid MessageFormId { get; set; }
        public MessageForm? MessageForm { get; set; }

        public Guid UserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}