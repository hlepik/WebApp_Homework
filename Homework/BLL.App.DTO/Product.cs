using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Resources;
using Domain.Base;


namespace BLL.App.DTO
{
    public class Product : DomainEntityId
    {

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Material")]
        public IEnumerable<string>? Material { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Description")]
        [MaxLength(500, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string Description { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Color")]
        [MaxLength(64, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(4, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        public string? Color { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "ProductAge")]
        public int? ProductAge { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "IsBooked")]
        public Boolean IsBooked { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "HasTransport")]
        public Boolean HasTransport { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Height")]
        public int? Height { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Width")]
        public int? Width { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Depth")]
        public int? Depth { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Condition")]
        public Guid ConditionId { get; set;}
        // public Condition? Conditions { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "County")]
        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]

        public Guid CountyId { get; set;}
        // public County? Counties { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Unit")]
        public Guid? UnitId { get; set;}
        // public Unit? Units { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Category")]
        [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "Required")]
        public Guid CategoryId { get; set;}
        // public Category? Categories { get; set; }

        // public ICollection<Booking>? Booking { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Picture")]
        public IEnumerable<string>? PictureUrls { get; set; }

        public Guid? CityId { get; set;}

        [MaxLength(500, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "LocationDescription")]
        public string? LocationDescription { get; set; }


        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "DateAdded")]
        [DataType(DataType.Date)]public DateTime DateAdded { get; set; }

        public Guid AppUserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "City")]
        public string? CityName { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "County")]
        public string? CountyName { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Unit")]
        public string? UnitName { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Category")]
        public string? CategoryName { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Condition")]
        public string? ConditionName { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Condition")]

        public Condition? Condition { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "City")]

        public City? City { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Category")]

        public Category? Category { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "County")]

        public County? County { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Unit")]


        public Unit? Unit { get; set; }


    }


}