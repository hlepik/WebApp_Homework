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
        public Boolean HasTransport { get; set; }
        public Guid AppUserId { get; set; }
        [MaxLength(64)] public string? Color { get; set; }
        [MaxLength(128)] public string? City { get; set; }
        [MaxLength(128)] public string? County { get; set; }
        [MaxLength(128)] public string? Category { get; set; }
        [MaxLength(500)] public string? LocationDescription { get; set; }
        [DataType(DataType.Date)]public DateTime DateAdded { get; set; }
        public string? Condition { get; set; }
        public IEnumerable<string>? Material { get; set; }
        public int? Width { get; set; }
        public int? ProductAge { get; set; }
        public int? Height { get; set; }
        public int? Depth { get; set; }
        public string? Unit { get; set; }
        public IEnumerable<string>? PictureUrls { get; set; }
    }

    public class ProductAdd
    {
        public Guid Id { get; set; }

        [MaxLength(500)] public string Description { get; set; } = default!;
        public Boolean IsBooked { get; set; }
        public Boolean HasTransport { get; set; }
        public Guid AppUserId { get; set; }
        [MaxLength(64)] public string? Color { get; set; }
        [MaxLength(128)] public Guid? City { get; set; }
        [MaxLength(128)] public Guid County { get; set; }
        [MaxLength(128)] public Guid Category { get; set; }
        [MaxLength(500)] public string? LocationDescription { get; set; }
        [DataType(DataType.Date)]public DateTime DateAdded { get; set; }
        public Guid Condition { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Depth { get; set; }
        public string? Unit { get; set; }
        public int? ProductAge { get; set; }
    }


}