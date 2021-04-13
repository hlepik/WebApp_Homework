using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Product
    {
        public Guid Id { get; set; }
        [MaxLength(500)] public string Description { get; set; } = default!;
        public Boolean IsBooked { get; set; }
        public Guid AppUserId { get; set; }

        [MaxLength(128)] public string? CityName { get; set; }
        [MaxLength(128)] public string? CountyName { get; set; }
        [MaxLength(500)] public string? LocationDescription { get; set; }
        public DateTime DateAdded { get; set; }
    }
}