using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Unit
    {
        public Guid Id { get; set; }
        public Guid NameId { get; set; }
        [MaxLength(54)] public string Name { get; set; } = default!;
    }
}