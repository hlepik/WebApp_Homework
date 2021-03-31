using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App.Identity
{
    public class UserMessages: DomainEntityId, IEnumerable
    {

        public Guid MessageFormId { get; set; }
        public MessageForm? MessageForm { get; set; }

        public Guid UserId { get; set; }
        public AppUser? AppUser { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}