using System;
using System.Collections.Generic;

namespace Domain
{
    public class ProductPictures
    {
        public Guid Id { get; set; }

        public Guid PictureId { get; set;}
        public Picture? Picture { get; set; }

        public Guid ProductId { get; set;}
        public Product? Product { get; set; }

    }
}