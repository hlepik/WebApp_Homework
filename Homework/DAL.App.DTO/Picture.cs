using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Picture : DomainEntityId
    {
        [MaxLength(500)] public string Url { get; set; } = default!;
        public string? ProductName { get; set; }
        public Guid? ProductOwner { get; set; }

        public Guid ProductId { get; set;}
        public Product? Product { get; set; }

    }
}