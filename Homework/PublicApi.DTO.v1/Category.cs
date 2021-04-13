using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Category
    {
        public Guid Id { get; set; }
        [MaxLength(128)] public string Name { get; set; } = default!;
    }
}