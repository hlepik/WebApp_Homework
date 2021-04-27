using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Booking : DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "City")]

        public string? City { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "County")]

        public string? County { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "LocationDescription")]

        public string? LocationDescription { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Width")]

        public int? Width { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Height")]

        public int? Height { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Depth")]

        public int? Depth { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Unit")]

        public string? Unit { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bookings), Name = "TimeBooked")]
        [DataType(DataType.Date)]
        public DateTime TimeBooked { get; set; } = DateTime.Now;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bookings), Name = "Until")]
        [DataType(DataType.Date)]
        public DateTime? Until { get; set; }

        public Guid ProductId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Product")]

        public string? Product { get; set; }
        public Guid AppUserId { get; set; }


    }
}