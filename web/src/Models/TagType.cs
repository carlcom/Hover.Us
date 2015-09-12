using System.Collections.Generic;

namespace web.Models
{
    public class TagType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}