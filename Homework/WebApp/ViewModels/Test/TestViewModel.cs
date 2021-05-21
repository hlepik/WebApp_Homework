using System.Collections;
using System.Collections.Generic;
using Domain.App;
using City = Domain.App.City;
#pragma warning disable 1591

namespace WebApp.ViewModels.Test
{
    public class TestViewModel
    {
        public ICollection<Product> Products { get; set; } = default!;
    }
}