using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DTO.App
{
    public class ProductDTO: DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        [MaxLength(500)] public string Description { get; set; } = default!;

        [MaxLength(64)] public string? Color { get; set; }

        public Boolean IsBooked { get; set; }
        public Boolean HasTransport { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public string County { get; set; } = default!;
        public string? City { get; set; }

        public string? LocationDescription { get; set; }

        public DateTime DateAdded { get; set; } = default!;

    }
}