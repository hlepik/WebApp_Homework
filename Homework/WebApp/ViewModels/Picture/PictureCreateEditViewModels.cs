using System;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Picture
{
    public class PictureCreateEditViewModels
    {
        public BLL.App.DTO.Picture Picture { get; set; } = default!;

        public SelectList? ProductSelectList { get; set; }

    }
}