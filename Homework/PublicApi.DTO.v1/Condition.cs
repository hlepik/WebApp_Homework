using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Condition
    {
        public Guid Id { get; set; }
        [MaxLength(1000)] public string Description { get; set; } = default!;
    }
}