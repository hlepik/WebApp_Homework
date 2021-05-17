using System.Collections;
using System.Collections.Generic;
using Domain.App;
using City = Domain.App.City;

namespace WebApp.ViewModels.Test
{
    public class TestViewModel
    {
        public ICollection<Product> Products { get; set; } = default!;
    }
}