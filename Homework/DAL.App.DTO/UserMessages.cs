using System;
using System.Collections;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;
using AppUser = DAL.App.DTO.Identity.AppUser;

namespace DAL.App.DTO
{
    public class UserMessages: DomainEntityId
    {

        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
        public DateTime DateSent { get; set; }

        public Guid AppUserId { get; set; }
        public string SenderEmail { get; set; } = default!;

    }
}