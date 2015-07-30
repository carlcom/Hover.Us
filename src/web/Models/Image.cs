using System;
using System.Collections.Generic;
using System.Drawing;
using web.Services;

namespace web.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTaken { get; set; }
        public bool Enabled { get; set; }
        public ICollection<ImageTag> ImageTags { get; set; }

        public string GetFile(string imageDirectory, Size size) => ImageRetriever.GetImage(imageDirectory, Name, size)
                                                                   ?? ImageCreator.CreateImage(imageDirectory, Name, size);
    }
}