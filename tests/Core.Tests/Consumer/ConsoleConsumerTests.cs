namespace Core.Tests.Consumer
{
    using Xunit;

    using RayTracer.Adapter;
    using RayTracingLib;
    using Core.Consumer;

    public class ConsoleConsumerTests
    {
        [Fact]
        public void NullableImageToString()
        {
            var consumer = new ConsoleConsumer();
            var image = new Image<Intensity>(3, 3);
            var bg = ConsoleAdapter.background;
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
