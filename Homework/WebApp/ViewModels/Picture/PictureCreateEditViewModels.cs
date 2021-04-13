
using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable 1591

namespace WebApp.ViewModels.Picture
{
    public class PictureCreateEditViewModels
    {
        public BLL.App.DTO.Picture Picture { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
    }
}
