using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Picture
    {
        public Guid Id { get; set; }
        [MaxLength(500)] public string Url { get; set; } = default!;

        public Guid ProductId { get; set;}
        public string ProductName { get; set;}= default!;
    }

}