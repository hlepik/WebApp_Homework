using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class MessageForm
    {
        public Guid Id { get; set; }
        [MaxLength(64)] public string Email { get; set; } = default!;

        [MaxLength(128)] public string Subject { get; set; } = default!;

        [MaxLength(1000)] public string Message { get; set; } = default!;

        public DateTime DateSent { get; set; } = DateTime.Now;

        public Guid? SenderId { get; set; }
    }
}