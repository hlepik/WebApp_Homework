using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Booking : DomainEntityId
    {

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bookings), Name = "TimeBooked")]
        [DataType(DataType.DateTime)]
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