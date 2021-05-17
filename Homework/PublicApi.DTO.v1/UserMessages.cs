using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class UserMessages
    {
        public Guid Id { get; set; }
        [MaxLength(64)] public string? Email { get; set; }
        [MaxLength(1000)] public string? Message { get; set; }
        [MaxLength(128)] public string? Subject { get; set; }
        public DateTime DateSent { get; set; }

        public Guid AppUserId { get; set; }

        [MaxLength(64)] public string SenderEmail { get; set; } = default!;
    }
}