using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class City
    {
        public Guid Id { get; set; }
        [MaxLength(128)] public string Name { get; set; } = default!;
    }
}