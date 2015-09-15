using System.Drawing;
using ImageResizer;
using Xunit;

namespace Test
{
    public class SizeExtensionsTests
    {
        [Fact]
        public void AspectRatioIsCalculatedFromDimensions()
        {
            var size = new Size(1280, 720);
            Assert.Equal(16.0 / 9, size.AspectRatio(), 4);
        }
    }
}