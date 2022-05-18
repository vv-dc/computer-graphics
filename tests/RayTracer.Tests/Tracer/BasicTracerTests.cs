namespace RayTracer.Tests.Tracer
{
    using System.Collections.Generic;

    using Xunit;

    using RayTracingLib;
    using RayTracingLib.Traceable;
    using RayTracingLib.Numeric;
    using RayTracer.Tracer;


    public class BasicTracerTests
    {
        [Fact]
        public void NoTraceForEmptyScene()
        {
            var ray = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, -1));
            var tracer = new BasicTracer(); // no scene objects

            HitResult? hitResult = tracer.Trace(ray);

            Assert.Null(hitResult);
        }

        [Fact]
        public void NoTraceForNonEmptyScene()
        {
            var ray = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, -1));
            var tracer = new BasicTracer();
            var sceneObjects = new List<ITraceable> {
                new Sphere(new Vector3(100, 100, 100), 1),
            };
            tracer.Init(sceneObjects);

            HitResult? hitResult = tracer.Trace(ray);

            Assert.Null(hitResult);
        }

        [Fact]
        public void IsTraceClosest()
        {
            var ray = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, -1));
            var tracer = new BasicTracer();
            var sceneObjects = new List<ITraceable> {
                new Sphere(new Vector3(0, 0, 0), 1),
                new Sphere(new Vector3(0, 0, 0), 2),
            };
            tracer.Init(sceneObjects);

            HitResult? hitResult = tracer.Trace(ray);

            Assert.NotNull(hitResult);
            Assert.Equal(1, hitResult!.distance); // distance to sphere surface (r)
            var expectedNormal = ray.GetPoint(hitResult!.distance) - new Vector3(0, 0, 0); // first sphere
            Assert.True(hitResult.Normal == expectedNormal);
        }
    }
}
