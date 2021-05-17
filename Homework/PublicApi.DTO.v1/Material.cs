using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Material
    {
        public Guid Id { get; set; }
        public Guid NameId { get; set; }
        [MaxLength(256)] public string Name { get; set; } = default!;

        [MaxLength(500)]public string? Comment { get; set; }
    }
}