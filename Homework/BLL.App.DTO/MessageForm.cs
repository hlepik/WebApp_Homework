using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class MessageForm : DomainEntityId
    {

        [MaxLength(64)] public string Email { get; set; } = default!;

        [MaxLength(128)] public string Subject { get; set; } = default!;

        [MaxLength(1000)] public string Message { get; set; } = default!;

        public DateTime DateSent { get; set; } = DateTime.Now;

        public Guid? SenderId { get; set; }

        public ICollection<UserMessages>? UserMessages { get; set; }



    }
}