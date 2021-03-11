using System;
using Domain.Base;

namespace Domain.App
{
    public class ProductPictures : DomainEntityId
    {

        public Guid PictureId { get; set;}
        public Picture? Picture { get; set; }

        public Guid ProductId { get; set;}
        public Product? Product { get; set; }

    }
}