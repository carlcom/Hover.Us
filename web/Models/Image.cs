using System;
using System.Collections.Generic;
using System.Drawing;
using VTSV.Services;

namespace VTSV.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTaken { get; set; }
        public bool Enabled { get; set; }
        public ICollection<ImageTag> ImageTags { get; set; }

        public string GetFile(string imageDirectory, Size size)
        {
            return ImageRetriever.GetImage(imageDirectory, Name, size)
                   ?? ImageCreator.CreateImage(imageDirectory, Name, size);
        }
    }
}