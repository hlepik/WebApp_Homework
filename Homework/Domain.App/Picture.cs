using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Picture : DomainEntityId
    {
        [MaxLength(500)] public string Url { get; set; } = default!;

        public Guid ProductId { get; set;}
        public Product? Product { get; set; }


    }
}