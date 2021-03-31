using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DTO.App
{
    public class MaterialDTO : DomainEntityId
    {
        [MaxLength(256)] public string Name { get; set; } = default!;
    }
}