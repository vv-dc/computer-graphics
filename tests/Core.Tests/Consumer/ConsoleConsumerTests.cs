namespace Core.Tests.Consumer
{
    using Xunit;

    using RayTracingLib;
    using Core.Consumer;

    public class ConsoleConsumerTests
    {
        [Fact]
        public void NullableImageToString()
        {
            var consumer = new ConsoleConsumer();
            var image = new Image<double?>(3, 3);
            image[0, 0] = null;
            image[0, 1] = -0.95;
            image[0, 2] = 0.1;
            image[1, 0] = 0.2;
            image[1, 1] = 0.3;
            image[1, 2] = 0.5;
            image[2, 0] = 0.6;
            image[2, 1] = 0.8;
            image[2, 2] = null;

            var actualStr = consumer.MapImageToString(image);
            var expectedStr =
            "  .\n" +
            "**O\n" +
            "O# \n";
            Assert.Equal(expectedStr, actualStr);
        }
    }
}
