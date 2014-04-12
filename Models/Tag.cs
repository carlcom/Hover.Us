using System.Collections.Generic;

namespace VTSV.Models
{
    public sealed class Tag
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public int ID { get; internal set; }
        public string Name { get; set; }
        public TagType Type { get; internal set; }
        public ICollection<Image> Images { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}