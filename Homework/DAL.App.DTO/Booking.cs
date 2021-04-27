using System;
using System.Collections.Generic;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Booking : DomainEntityId
    {

        public string? City { get; set; }
        public string? County { get; set; }
        public string? LocationDescription { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Depth { get; set; }
        public string? Unit { get; set; }

        public DateTime TimeBooked { get; set; } = DateTime.Now;
        public DateTime? Until { get; set; }
        public Guid ProductId { get; set; }
        public string? Product { get; set; }
        public Guid AppUserId { get; set; }

    }

}