using System.Drawing;

namespace web.Services
{
    public static class ImageSizer
    {
        public static Size GetNewSize(Size image, Size screen)
        {
            //first make sure we're not trying to enlarge the image
            if ((image.Width < screen.Width && image.Height < screen.Height)
                || (image.Width < screen.Height && image.Height < screen.Width))
                return image;

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