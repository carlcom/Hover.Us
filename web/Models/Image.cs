using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTaken { get; set; }
        public bool Enabled { get; set; }
        public ICollection<ImageTag> ImageTags { get; set; }


        public string Tag(string type)
        {
            return ImageTags.Select(it => it.Tag).First(t => t.TagType.Name == type).Name;
        }
    }
}