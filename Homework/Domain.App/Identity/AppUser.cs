using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(128, MinimumLength = 1)]
        [Display(ResourceType = typeof(Resources.Views.AppUser.AppUser), Name = "Firstname")]

        public string Firstname { get; set; } = default!;
        [StringLength(128, MinimumLength = 1)]
        [Display(ResourceType = typeof(Resources.Views.AppUser.AppUser), Name = "Lastname")]

        public string Lastname { get; set; } = default!;


        // public string FullName => Firstname + " " + Lastname;
        // public string FullNameEmail => FullName + " (" + Email + ")";

    }
}