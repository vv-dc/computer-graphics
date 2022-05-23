namespace RayTracingLib.Tests.Traceable
{
    using System;

    using Xunit;

    using Common;
    using Common.Numeric;
    using RayTracingLib.Traceable;

    public class CylinderTests
    {
        private readonly Cylinder cylinder;

        public CylinderTests()
        {
            cylinder = new Cylinder(new Point3(2, 0, 2), new Point3(2), 1);
        }

        public static void AssertNoIntersection(Cylinder cylinder, Ray ray)
        {
            Assert.False(cylinder.Intersect(ray, out var hitResult));
            Assert.Null(hitResult);
        }

        [Fact]
        public void RayLookInOppositeDirection()
        {
            var ray = new Ray(new Point3(0, 1, 0), new Vector3(-1, 0, -1));
            AssertNoIntersection(cylinder, ray);
        }

        [Fact]
        public void RayLookInsideCylinderButDoesNotIntersectIt()
        {
            var ray = new Ray(new Point3(2, 0, 2), new Vector3(0.1F, 1, 0.1F));
            AssertNoIntersection(cylinder, ray);
        }

        [Fact]
        public void RayIntersectInfiniteCylinderButNotBounded()
        {
            var ray = new Ray(new Point3(0), new Vector3(1, 2, 1));
            AssertNoIntersection(cylinder, ray);
        }

        public static void AssertIntersection(Cylinder cylinder, Ray ray, float distance)
        {
            Assert.True(cylinder.Intersect(ray, out var hitResult));
            Assert.Equal(distance, hitResult!.distance, Consts.PRECISION);
        }

        [Fact]
        public void RayIntersectsOutsideSurface()
        {
            var ray = new Ray(new Point3(0.5F, 0, 2), new Vector3(0.5F, 1, 0));
            var distance = MathF.Sqrt(1.25F);
            AssertIntersection(cylinder, ray, distance);
        }

        [Fact]
        public void RayIntersectsInsideSurface()
        {
            var ray = new Ray(new Point3(2, 0, 2), new Vector3(0, 1, 1));
            var distance = MathF.Sqrt(2);
            AssertIntersection(cylinder, ray, distance);
        }

        [Fact]
        public void RayIntersectsBothSurface()
        {
            var ray = new Ray(new Point3(2, 1, 0), new Vector3(0, 0, 1));
            var distance = 1F;
            AssertIntersection(cylinder, ray, distance);
        }
    }
}
