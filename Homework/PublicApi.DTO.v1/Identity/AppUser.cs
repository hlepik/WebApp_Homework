using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;

namespace PublicApi.DTO.v1.Identity
{
    public class AppUser
    {

    [StringLength(128, MinimumLength = 1)] public string Firstname { get; set; } = default!;
    [StringLength(128, MinimumLength = 1)] public string Lastname { get; set; } = default!;
    public Guid Id { get; set; }

    [StringLength(128, MinimumLength = 1)] public string Email { get; set; } = default!;
    [StringLength(128, MinimumLength = 1)] public string UserName { get; set; } = default!;
    }
}