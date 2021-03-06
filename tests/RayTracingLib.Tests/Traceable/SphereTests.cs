namespace RayTracingLib.Tests.Traceable
{
    using System;

    using Xunit;

    using Common;
    using Common.Numeric;
    using RayTracingLib.Traceable;

    public class SphereTests
    {
        private readonly Sphere sphere;

        public SphereTests()
        {
            sphere = new Sphere(new Point3(1), 3);
        }

        public static void AssertNoIntersection(Sphere sphere, Ray ray)
        {
            Assert.False(sphere.Intersect(ray, out var hitResult));
            Assert.Null(hitResult);
        }

        [Fact]
        public void SphereInOppositeDirection()
        {
            var ray = new Ray(new Point3(5), new Vector3(1));
            AssertNoIntersection(sphere, ray);
        }

        [Fact]
        public void RayLineDoesNotIntersectSphere()
        {
            var ray = new Ray(new Point3(5), new Vector3(0, 0, 1));
            AssertNoIntersection(sphere, ray);
        }

        [Fact]
        public void RayOriginLiesOnSphereAndRayLookOutside()
        {
            var ray = new Ray(new Point3(1, 1, 4), new Vector3(1));
            AssertNoIntersection(sphere, ray);
        }

        public static void AssertIntersection(Sphere sphere, Ray ray, float distance)
        {
            Assert.True(sphere.Intersect(ray, out var hitResult));
            Assert.Equal(distance, hitResult!.distance, Consts.PRECISION);
        }

        [Fact]
        public void OneIntersectionPoint()
        {
            var ray = new Ray(new Point3(0, 0, -2), new Vector3(1, 1, 0));
            var distance = MathF.Sqrt(2);
            AssertIntersection(sphere, ray, distance);
        }

        [Fact]
        public void TwoIntersectionPoints()
        {
            var ray = new Ray(new Point3(-3), new Vector3(1));
            var distance = 4 * MathF.Sqrt(3) - 3;
            AssertIntersection(sphere, ray, distance);
        }

        [Fact]
        public void RayOriginInsideSphere()
        {
            var ray = new Ray(new Point3(0), new Vector3(1));
            var distance = 3 + MathF.Sqrt(3);
            AssertIntersection(sphere, ray, distance);
        }

        [Fact]
        public void RayOriginLiesOnSphereAndRayLookInside()
        {
            var ray = new Ray(new Point3(1, 1, 4), new Vector3(0, 0, -1));
            var distance = 6F;
            AssertIntersection(sphere, ray, distance);
        }
    }
}
