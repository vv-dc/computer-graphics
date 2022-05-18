namespace RayTracingLib.Tests.Traceable
{
    using System;
    using Xunit;

    using RayTracingLib;
    using RayTracingLib.Traceable;
    using RayTracingLib.Numeric;

    public class TriangleTests
    {
        private readonly Triangle triangle;

        public TriangleTests()
        {
            triangle = new Triangle(
                new Vector3(1, 0, 0), // A (v0)
                new Vector3(0, 0, 1), // B (v1)
                new Vector3(0, 1, 0)  // C (v2)
            ); // look at origin
        }

        public static void AssertNoIntersection(Triangle triangle, Ray ray)
        {
            Assert.False(triangle.Intersect(ray, out var hitResult));
            Assert.Null(hitResult);
        }

        [Fact]
        public void TriangleInOppositeDirection()
        {
            var ray = new Ray(new Vector3(5), new Vector3(1));
            AssertNoIntersection(triangle, ray);
        }

        [Fact]
        public void RayPassesNearTriangleEdge()
        {
            var ray = new Ray(new Vector3(0), new Vector3(1, -1E-3f, 1));
            AssertNoIntersection(triangle, ray);
        }

        [Fact]
        public void RayLookInAnotherDirection()
        {
            var ray = new Ray(new Vector3(0), new Vector3(-1, 1, 1));
            AssertNoIntersection(triangle, ray);
        }

        public static void AssertIntersection(Triangle triangle, Ray ray, float distance)
        {
            Assert.True(triangle.Intersect(ray, out var hitResult));
            Assert.Equal(distance, hitResult!.distance, Consts.PRECISION);
        }

        [Fact]
        public void RayIntersectsTriangleEdge()
        {
            var ray = new Ray(new Vector3(0), new Vector3(1, 1, 0));
            var distance = 1 / (float)Math.Sqrt(2);
            AssertIntersection(triangle, ray, distance);
        }

        [Fact]
        public void RayIntersectsTriangleVertex()
        {
            var ray = new Ray(new Vector3(0), new Vector3(1, 0, 0));
            var distance = 1F;
            AssertIntersection(triangle, ray, distance);
        }

        [Fact]
        public void RayIntersectsTriangleSurface()
        {
            var ray = new Ray(new Vector3(0), new Vector3(1));
            var distance = 1 / (float)Math.Sqrt(3);
            AssertIntersection(triangle, ray, distance);
        }
    }
}