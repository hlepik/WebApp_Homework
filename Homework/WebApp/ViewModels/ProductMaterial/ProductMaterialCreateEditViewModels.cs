using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.ProductMaterial
{
    public class ProductMaterialCreateEditViewModels
    {
        public Domain.App.ProductMaterial ProductMaterial { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
        public SelectList? MaterialSelectList { get; set; }
    }
}