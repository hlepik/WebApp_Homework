using System;

namespace PublicApi.DTO.v1
{
    public class ProductMaterial
    {
        public Guid Id { get; set; }
        public Guid MaterialId { get; set;}
        public Guid ProductId { get; set;}
        public string? ProductName { get; set;}
        public string? MaterialName { get; set;}
    }
}