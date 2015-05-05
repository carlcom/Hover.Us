using System.Drawing;
using VTSV.Services;
using Xunit;

namespace tests
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