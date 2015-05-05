using System.Collections.Generic;

namespace VTSV.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public TagType TagType { get; set; }
        public ICollection<ImageTag> ImageTags { get; set; }
        protected int Type_ID { get; set; }
    }
}