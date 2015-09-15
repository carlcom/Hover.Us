using System.Collections.Generic;

namespace Web.Models
{
    public class TagType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}