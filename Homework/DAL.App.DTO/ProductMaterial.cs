using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ProductMaterial : DomainEntityId
    {

        public string? Product { get; set; }
        public string? Material { get; set; }
        public Guid ProductOwner { get; set;}

        public Guid MaterialId { get; set;}

        public Guid ProductId { get; set;}



    }
}