using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable 1591

namespace WebApp.ViewModels.ProductMaterial
{
    public class ProductMaterialCreateEditViewModels
    {
        public BLL.App.DTO.ProductMaterial ProductMaterial { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
        public SelectList? MaterialSelectList { get; set; }
    }
}