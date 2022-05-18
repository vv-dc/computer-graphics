namespace RayTracingLib.Tests.Traceable
{
    using System;
    using Xunit;

    using RayTracingLib;
    using RayTracingLib.Traceable;
    using RayTracingLib.Numeric;

    public class PlaneTests
    {
        private readonly Plane plane;

        public PlaneTests()
        {
            plane = new Plane(new Vector3(1), new Point3(1));
        }

        public static void AssertNoIntersection(Plane plane, Ray ray)
        {
            Assert.False(plane.Intersect(ray, out var hitResult));
            Assert.Null(hitResult);
        }

        [Fact]
        public void PlaneInOppositeDirection()
        {
            var ray = new Ray(new Point3(0), new Vector3(-1));
            AssertNoIntersection(plane, ray);
        }

        [Fact]
        public void RayParalellToPlane()
        {
            var ray = new Ray(new Point3(0), new Vector3(-1, 1, 0));
            AssertNoIntersection(plane, ray);
        }

        [Fact]
        public void RayLiesOnPlane()
        {
            var ray = new Ray(new Point3(0, 0, 3), new Vector3(1, -1, 0));
            AssertNoIntersection(plane, ray);
        }

        [Fact]
        public void RayOriginLiesOnPlane()
        {
            var ray = new Ray(new Point3(3, 0, 0), new Vector3(1, 0, 1));
            AssertNoIntersection(plane, ray);
        }

        public static void AssertIntersection(Plane plane, Ray ray, float distance)
        {
            Assert.True(plane.Intersect(ray, out var hitResult));
            Assert.Equal(distance, hitResult!.distance, Consts.PRECISION);
        }

        [Fact]
        public void RayIntersectsPlane()
        {
            var ray = new Ray(new Point3(0), new Vector3(1));
            var distance = (float)Math.Sqrt(3);
            AssertIntersection(plane, ray, distance);
        }
    }
}