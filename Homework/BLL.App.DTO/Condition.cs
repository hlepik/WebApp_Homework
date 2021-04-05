using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Condition : DomainEntityId
    {


        [MaxLength(1000)] public string Description { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }
    }
}