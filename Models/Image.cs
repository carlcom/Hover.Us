using System;
using System.Collections.Generic;

namespace VTSV.Models
{
    public class Image
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime? DateFeatured { get; set; }
        public bool Enabled { get; set; }
        public ICollection<Tag> Tags { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}