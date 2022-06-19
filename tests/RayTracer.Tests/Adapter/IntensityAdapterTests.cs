namespace RayTracer.Tests.Adapter
{
    using Xunit;

    using System.Collections.Generic;
    using Common.Numeric;
    using RayTracer.Adapter;
    using RayTracingLib;
    using RayTracingLib.Light;

    public class IntensityAdapterTests
    {
        [Fact]
        public void CalculateDot()
        {
            var adapter = new IntensityAdapter();
            var light = new DirectionalLight(new Vector3(0, 0, -1), Color.White);
            var hitResult = new HitResult() { Normal = new Vector3(0, 0, 1) };

            var actual = adapter.Adapt(new List<Light> { light }, hitResult);
            var expected = -Vector3.Dot(light.Direction, hitResult.Normal);
            Assert.Equal(actual.Value, expected);
        }
    }
}
