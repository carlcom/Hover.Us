using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTaken { get; set; }
        public bool Enabled { get; set; }
        public ICollection<ImageTag> ImageTags { get; set; }
    }
}