using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DTO.App
{
    public class CategoryDTO : DomainEntityId
    {
        [MaxLength(128)] public string Name { get; set; } = default!;

    }
}