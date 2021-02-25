using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class MessageForm
    {
        public Guid Id { get; set; }

        [MaxLength(64)] public String Email { get; set; } = default!;

        [MaxLength(128)] public String Subject { get; set; } = default!;

        [MaxLength(1000)] public String Message { get; set; } = default!;

        public ICollection<UserMessage>? UserMessages { get; set; }
    }
}