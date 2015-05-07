using System.Drawing;

namespace VTSV.Services
{
    public static class SizeExtensions
    {
        public static double AspectRatio(this Size size)
        {
            return (double) size.Width/size.Height;
        }
    }
}