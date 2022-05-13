namespace RayTracer.Tests.Adapter
{
    using Xunit;

    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracingLib.Numeric;
    using RayTracer.Adapter;

    public class ConsoleAdapterTests
    {
        [Fact]
        public void CalculateDot()
        {
            var adapter = new ConsoleAdapter();
            var light = new DirectionalLight(new Vector3(0, 0, -1));
            var hitResult = new HitResult() { Normal = new Vector3(0, 0, 1) };

            var actual = adapter.Adapt(light, hitResult);
            var expected = -Vector3.Dot(light.Direction, hitResult.Normal);
            Assert.Equal(actual, expected);
        }
    }
}