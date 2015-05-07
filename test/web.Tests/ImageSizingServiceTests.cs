using System.Drawing;
using VTSV.Services;
using Xunit;

namespace tests
{
    public class ImageSizingServiceTests
    {
        [Fact]
        public void LandscapeImageIsWiderThanLandscapeScreen()
        {
            var image = new Size(2000, 1000);
            var screen = new Size(1280, 720);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(1280, 640), newSize);
        }

        [Fact]
        public void LandscapeImageIsTallerThanLandscapeScreen()
        {
            var image = new Size(1500, 1000);
            var screen = new Size(1280, 720);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(1080, 720), newSize);
        }

        [Fact]
        public void PortraitImageIsTallerThanLandscapeScreen()
        {
            var image = new Size(1000, 1500);
            var screen = new Size(1280, 720);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(720, 1080), newSize);
        }

        [Fact]
        public void PortraitImageIsWiderThanLandscapeScreen()
        {
            var image = new Size(1000, 2000);
            var screen = new Size(1280, 720);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(640, 1280), newSize);
        }
        [Fact]
        public void LandscapeImageIsWiderThanPortraitScreen()
        {
            var image = new Size(2000, 1000);
            var screen = new Size(720, 1280);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(1280, 640), newSize);
        }

        [Fact]
        public void LandscapeImageIsTallerThanPortraitScreen()
        {
            var image = new Size(1500, 1000);
            var screen = new Size(720, 1280);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(1080, 720), newSize);
        }

        [Fact]
        public void PortraitImageIsTallerThanPortraitScreen()
        {
            var image = new Size(1000, 1500);
            var screen = new Size(720, 1280);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(720, 1080), newSize);
        }

        [Fact]
        public void PortraitImageIsWiderThanPortraitScreen()
        {
            var image = new Size(1000, 2000);
            var screen = new Size(720, 1280);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(640, 1280), newSize);
        }

        [Fact]
        public void LandscapeImageCoversThumbnail()
        {
            var image = new Size(2000, 1000);
            var screen = new Size(256, 256);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(512, 256), newSize);
        }

        [Fact]
        public void PortraitImageCoversThumbnail()
        {
            var image = new Size(1000, 2000);
            var screen = new Size(256, 256);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(256, 512), newSize);
        }

        [Fact]
        public void imageWillNotBeEnlarged()
        {
            var image = new Size(720, 480);
            var screen = new Size(1280, 720);
            var newSize = ImageSizer.GetNewSize(image, screen);
            Assert.Equal(new Size(720,480), newSize);
        }
    }
}