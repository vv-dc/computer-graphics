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
            var ray = new Ray(new Point3(0), new Vector3(0, 0, -1));
            var tracer = new BasicTracer(); // no scene objects

            var hit = tracer.Trace(ray, out var hitResult);

            Assert.False(hit);
            Assert.Null(hitResult);
        }

        [Fact]
        public void NoTraceForNonEmptyScene()
        {
            var ray = new Ray(new Point3(0), new Vector3(0, 0, -1));
            var tracer = new BasicTracer();
            var sceneObjects = new List<ITraceable> {
                new Sphere(new Point3(100), 1),
            };
            tracer.Init(sceneObjects);

            var hit = tracer.Trace(ray, out var hitResult);

            Assert.False(hit);
            Assert.Null(hitResult);
        }

        [Fact]
        public void IsTraceClosest()
        {
            var ray = new Ray(new Point3(0), new Vector3(0, 0, -1));
            var tracer = new BasicTracer();
            var sceneObjects = new List<ITraceable> {
                new Sphere(new Point3(0), 1),
                new Sphere(new Point3(0), 2),
            };
            tracer.Init(sceneObjects);

            var hit = tracer.Trace(ray, out var hitResult);

            Assert.True(hit);
            Assert.NotNull(hitResult);

            Assert.Equal(1, hitResult!.distance); // distance to sphere surface (r)
            var expectedNormal = ray.GetPoint(hitResult!.distance) - new Point3(0); // first sphere
            Assert.True(hitResult.Normal == expectedNormal);
        }
    }
}
