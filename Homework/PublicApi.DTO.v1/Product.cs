using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Product
    {
        public Guid Id { get; set; }

        public IEnumerable<string>? Material { get; set; }

        [MinLength(2), MaxLength(128)]
        public string Description { get; set; } = default!;

        [MinLength(4), MaxLength(64)]
        public string? Color { get; set; }
        public int? ProductAge { get; set; }
        public Boolean IsBooked { get; set; }
        public Boolean HasTransport { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? Depth { get; set; }
        public Guid ConditionId { get; set;}
        public Guid CountyId { get; set;}
        public Guid? UnitId { get; set;}

        public Guid CategoryId { get; set;}

        public IEnumerable<string>? PictureUrls { get; set; }

        public Guid? CityId { get; set;}

        [MinLength(2), MaxLength(500)]
        public string? LocationDescription { get; set; }

        [DataType(DataType.Date)]public DateTime DateAdded { get; set; }

        public Guid AppUserId { get; set; }

        public string? CityName { get; set; }

        public string? CountyName { get; set; }
        public string? UnitName { get; set; }
        public string? CategoryName { get; set; }

        public string? ConditionName { get; set; }

    }



}