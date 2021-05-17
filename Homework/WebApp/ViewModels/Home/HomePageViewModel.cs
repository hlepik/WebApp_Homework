
using System.Collections;
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591

namespace WebApp.ViewModels.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<Product> LastInsertedProducts { get; set; } = default!;

        public Product Product { get; set; } = default!;
        public SelectList? CategorySelectList { get; set; }
        public SelectList? CountySelectList { get; set; }
        public SelectList? CitySelectList { get; set; }
    }
}
