using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class Product : DomainEntityId
    {

        public string? Material { get; set; }
        [MaxLength(500)] public string Description { get; set; } = default!;

        [MaxLength(64)] public string? Color { get; set; }

        public int? ProductAge { get; set; }
        public Boolean IsBooked { get; set; }
        public Boolean HasTransport { get; set; }

        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? Depth { get; set; }


        public Guid ConditionId { get; set;}
        public Condition? Condition { get; set; }

        public Guid CountyId { get; set;}
        public County? County { get; set; }

        public Guid? UnitId { get; set;}
        public Unit? Unit { get; set; }

        public Guid CategoryId { get; set;}
        public Category? Category { get; set; }

        public ICollection<Booking>? Booking { get; set; }


        public Guid? CityId { get; set;}
        public City? City { get; set; }

        [MaxLength(500)]
        public string? LocationDescription { get; set; }

        public ICollection<Picture>? Pictures { get; set; }


        public ICollection<ProductMaterial>? ProductMaterials { get; set; }

        public DateTime DateAdded { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string? CityName { get; set; }
        public string? CountyName { get; set; }
        public string? UnitName { get; set; }
        public string? CategoryName { get; set; }
        public string? ConditionName { get; set; }


    }
}