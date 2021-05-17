using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class MessageForm : DomainEntityId
    {

        [MaxLength(64)] public string Email { get; set; } = default!;

        [MaxLength(128)] public string Subject { get; set; } = default!;

        [MaxLength(1000)] public string Message { get; set; } = default!;

        [DataType(DataType.DateTime)]
        public DateTime DateSent { get; set; }

        public Guid? SenderId { get; set; }

    }
}