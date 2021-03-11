using System;
using Domain.Base;

namespace Domain.App
{
    public class UserMessage : DomainEntityId
    {

        public DateTime DateSent { get; set; } = DateTime.Now;

        public Guid UserId { get; set;}
        public User? User { get; set; }

        public Guid MessageFormId { get; set;}
        public MessageForm? MessageForm { get; set; }
    }
}