using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class County
    {
        public Guid Id { get; set; }
        public Guid NameId { get; set; }
        [MaxLength(128)] public string Name { get; set; } = default!;
    }
}