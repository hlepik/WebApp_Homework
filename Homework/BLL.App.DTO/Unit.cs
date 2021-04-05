using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Unit : DomainEntityId
    {

        [MaxLength(54)] public string Name { get; set; } = default!;


        public ICollection<Product>? Products { get; set; }

    }
}