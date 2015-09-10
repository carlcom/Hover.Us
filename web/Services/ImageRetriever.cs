using System.Drawing;
using System.IO;

namespace web.Services
{
    public static class ImageRetriever
    {
        public static string GetImage(string imageDirectory, string imageName, Size screenSize)
        {
            var files = Directory.EnumerateFiles(Path.Combine(imageDirectory, "web"), imageName + "*");
            var maxArea = 0;
            var maxFile = "";
            foreach (var f in files)
            {
                var sizeString = f.Split('-')[1].Split('.')[0];
                var dimensions = sizeString.Split('x');
                var width = int.Parse(dimensions[0]);
                var height = int.Parse(dimensions[1]);
                if ((screenSize.Width == width || screenSize.Height == height
                     || screenSize.Width == height || screenSize.Height == width)
                    && width*height > maxArea)
                {
                    maxArea = width*height;
                    maxFile = f;
                }
            }
            return maxFile != string.Empty ? Path.Combine(imageDirectory, maxFile) : null;
        }
    }
}