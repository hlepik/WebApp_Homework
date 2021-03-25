using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Picture
{
    public class PictureCreateEditViewModels
    {
        public Domain.App.Picture Picture { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }
    }
}