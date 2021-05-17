using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserMessages: DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [MaxLength(128)] public string Subject { get; set; } = default!;

        [MaxLength(1000)] public string Message { get; set; } = default!;

        public string SenderEmail { get; set; } = default!;

        [DataType(DataType.DateTime)]
        public DateTime DateSent { get; set; }


    }
}