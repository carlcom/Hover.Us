using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace VTSV.Services
{
    internal static class ImageCreator
    {
        public static string CreateImage(string imageDirectory, string imageName, Size screenSize)
        {
            var bitmap = getOriginal(imageDirectory, imageName);
            var imageSize = new Size(bitmap.Width, bitmap.Height);
            var newSize = ImageSizer.GetNewSize(imageSize, screenSize);
            return saveImage(bitmap, newSize, imageDirectory, imageName);
        }

        private static Bitmap getOriginal(string imageDirectory, string imageName)
        {
            var fileName = imageName + ".jpg";
            var path = Path.Combine(imageDirectory, "full", fileName);
            var bitmap = new Bitmap(path);
            return bitmap;
        }

        private static string saveImage(Bitmap bitmap, Size newSize, string imageDirectory, string imageName)
        {
            var newImage = new Bitmap(newSize.Width, newSize.Height);
            var graphics = Graphics.FromImage(newImage);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(bitmap, 0, 0, newSize.Width, newSize.Height);

            var newFileName = imageName + "-" + newSize.Width + "x" + newSize.Height + ".jpg";
            var newPath = Path.Combine(imageDirectory, "web", newFileName);
            newImage.Save(newPath, ImageFormat.Jpeg);
            return newPath;
        }
    }
}