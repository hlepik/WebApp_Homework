using System;
using System.Collections.Generic;
using Domain.App.Identity;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.ViewModels
{
    public class AppUserAddRoleViewModel
    {

        public List<AppUser>? Members { get; set; }
        public List<AppUser>? NonMembers { get; set; }
        public AppRole Role { get; set; }= default!;


        public Guid[] AddIds { get; set; }= default!;

        public Guid[] DeleteIds { get; set; }= default!;


    }
}