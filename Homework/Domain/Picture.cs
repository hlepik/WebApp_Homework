using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Picture
    {
        public Guid Id { get; set; }

        [MaxLength(500)] public String Url { get; set; } = default!;


        public ICollection<ProductPictures>? ProductPicturesCollection { get; set; }


    }
}