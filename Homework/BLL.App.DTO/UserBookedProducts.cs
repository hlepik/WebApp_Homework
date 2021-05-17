using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO
{

    public class UserBookedProducts : DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bookings), Name = "Until")]
        public DateTime? Until { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "LocationDescription")]
        public string? LocationDescription { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Color")]
        public string? Color { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bookings), Name = "TimeBooked")]
        public DateTime TimeBooked { get; set; }
        public Guid AppUserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Description")]
        public string Description { get; set; }= default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.MessageForms), Name = "Email")]
        public string Email { get; set; }= default!;
        public Guid BookingId { get; set;}
        public Booking? Booking { get; set;}
        public Guid ProductId { get; set; }

    }
}