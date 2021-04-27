﻿using System;
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

        public Guid ConditionId { get; set;}
        public Condition? Conditions { get; set; }
        public Guid CountyId { get; set;}
        public County? Counties { get; set; }
        public Guid? UnitId { get; set;}
        public Unit? Units { get; set; }
        public Guid CategoryId { get; set;}
        public Category? Categories { get; set; }

        public ICollection<Booking>? Booking { get; set; }

        public Guid? CityId { get; set;}

        [MaxLength(500, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength"),
         MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "LocationDescription")]
        public string? LocationDescription { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Picture")]
        public ICollection<Picture>? Pictures { get; set; }

        public ICollection<ProductMaterial>? ProductMaterials { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "DateAdded")]
        [DataType(DataType.Date)]public DateTime DateAdded { get; set; }

        public Guid AppUserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "City")]
        public string? City { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "County")]
        public string? County { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Unit")]
        public string? Unit { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Category")]
        public string? Category { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Products), Name = "Condition")]
        public string? Condition { get; set; }

    }


}