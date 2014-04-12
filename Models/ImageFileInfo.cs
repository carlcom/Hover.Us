using System;
using System.IO;
using VTSV.Properties;

namespace VTSV.Models
{
    public sealed class ImageFileInfo
    {
        public ImageFileInfo(string filename)
        {
            var dash = filename.Split('-');
            Name = dash[0];
            var ex = dash[1].Split('x');
            x = Convert.ToInt16(ex[0]);
            y = Convert.ToInt16(ex[1]);
        }

        public ImageFileInfo()
        {
            // TODO: Complete member initialization
        }

        public string Name { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public string ImagePath { get { return Path.Combine(Settings.Default.ImagePath, "web", Name + (x > 0 & y > 0 ? "-" + x + "x" + y : "") + ".jpg"); } }
    }
}