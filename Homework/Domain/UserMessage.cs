using System;

namespace Domain
{
    public class UserMessage
    {
        public Guid Id { get; set; }

        public DateTime DateSent { get; set; } = DateTime.Now;

        public Guid UserId { get; set;}
        public User? User { get; set; }

        public Guid MessageFormId { get; set;}
        public MessageForm? MessageForm { get; set; }
    }
}