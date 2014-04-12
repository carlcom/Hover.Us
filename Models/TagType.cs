using System.Collections.Generic;

namespace VTSV.Models
{
    public class TagType
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public ICollection<Tag> Tags { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}