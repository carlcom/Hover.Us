using System.Drawing;

namespace web.Services
{
    public static class SizeExtensions
    {
        public static double AspectRatio(this Size size) => (double) size.Width/size.Height;
    }
}