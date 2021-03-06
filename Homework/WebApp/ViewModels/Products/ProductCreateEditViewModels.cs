using System.ComponentModel.DataAnnotations;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable 1591

namespace WebApp.ViewModels.Products
{
    public class ProductCreateEditViewModels
    {
        public Product Product { get; set; } = default!;

        public SelectList? ConditionSelectList { get; set; }
        public SelectList? CountySelectList { get; set; }
        public SelectList? CitySelectList { get; set; }

        public SelectList? UnitSelectList { get; set; }
        public SelectList? CategorySelectList { get; set; }

    }
}