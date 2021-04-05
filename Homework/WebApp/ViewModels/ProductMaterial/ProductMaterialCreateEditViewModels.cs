using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.ProductMaterial
{
    public class ProductMaterialCreateEditViewModels
    {
        public BLL.App.DTO.ProductMaterial ProductMaterial { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
        public SelectList? MaterialSelectList { get; set; }
    }
}