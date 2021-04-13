using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Booking : DomainEntityId
    {
        public string? ProductName { get; set; }
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
        public string Product { get; set; } = default!;
        public Guid AppUserId { get; set; }


    }
}