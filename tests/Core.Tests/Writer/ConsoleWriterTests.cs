namespace Core.Tests.Writer
{
    using Xunit;

    using Core.Writer;
    using RayTracer;

    public class ConsoleWriterTests
    {
        [Fact]
        public void NullableImageToString()
        {
            var consumer = new ConsoleWriter();
            var image = new Image<Intensity>(3, 3);
            var bg = Intensity.Background;
            image[0, 0] = bg;
            image[0, 1] = -0.95f;
            image[0, 2] = 0.1f;
            image[1, 0] = 0.2f;
            image[1, 1] = 0.3f;
            image[1, 2] = 0.5f;
            image[2, 0] = 0.6f;
            image[2, 1] = 0.8f;
            image[2, 2] = bg;

            var actualStr = consumer.MapImageToString(image);
            var expectedStr =
            "- .\n" +
            "**O\n" +
            "O#-\n";
            Assert.Equal(expectedStr, actualStr);
        }
    }
}
