using System.Drawing;
using web.Services;
using Xunit;

namespace web.Tests.Services
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