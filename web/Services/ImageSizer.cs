using System.Drawing;

namespace VTSV.Services
{
    public class ImageSizer
    {
        public static Size GetNewSize(Size image, Size screen)
        {
            //screen orientation matches image orientation
            if ((screen.AspectRatio() > 1 && image.AspectRatio() > 1)
                || (screen.AspectRatio() < 1 && image.AspectRatio() < 1))
            {
                return image.AspectRatio() > screen.AspectRatio()
                    ? new Size(screen.Width, (int) (screen.Width/image.AspectRatio()))
                    : new Size((int) (screen.Height*image.AspectRatio()), screen.Height);
            }

            //use opposite orientation
            if ((screen.AspectRatio() > 1 && image.AspectRatio() < 1)
                || (screen.AspectRatio() < 1 && image.AspectRatio() > 1))
            {
                return image.AspectRatio() > 1/screen.AspectRatio()
                    ? new Size(screen.Height, (int) (screen.Height/image.AspectRatio()))
                    : new Size((int) (screen.Width*image.AspectRatio()), screen.Width);
            }

            //thumbnails should cover instead of contain
            return image.AspectRatio() > 1
                ? new Size((int) (screen.Width*image.AspectRatio()), screen.Width)
                : new Size(screen.Width, (int) (screen.Width/image.AspectRatio()));
        }
    }
}