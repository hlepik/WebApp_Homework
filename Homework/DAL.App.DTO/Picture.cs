using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Picture : DomainEntityId
    {
        [MaxLength(500)] public string Url { get; set; } = default!;
        public string Product{ get; set; } = default!;
        public Guid? ProductOwner { get; set; }

        public Guid ProductId { get; set;}


    }
}