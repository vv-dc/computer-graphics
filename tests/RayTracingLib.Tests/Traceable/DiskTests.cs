namespace RayTracingLib.Tests.Traceable
{
    using System;

    using Xunit;

    using Common;
    using Common.Numeric;
    using RayTracingLib.Traceable;

    public class DiskTests
    {
        private readonly Disk disk;

        public DiskTests()
        {
            disk = new Disk(new Vector3(0, 1, 0), new Point3(1), 1);
        }

        public static void AssertNoIntersection(Disk disk, Ray ray)
        {
            Assert.False(disk.Intersect(ray, out var hitResult));
            Assert.Null(hitResult);
        }

        [Fact]
        public void RayIntersectPlaneButPassDiskSurface()
        {
            var ray = new Ray(new Point3(0), new Vector3(0, 1, 0));
            AssertNoIntersection(disk, ray);
        }

        [Fact]
        public void RayDoesNotIntersectPlane()
        {
            var ray = new Ray(new Point3(0), new Vector3(-1));
            AssertNoIntersection(disk, ray);
        }

        public static void AssertIntersection(Disk disk, Ray ray, float distance)
        {
            Assert.True(disk.Intersect(ray, out var hitResult));
            Assert.Equal(distance, hitResult!.distance, Consts.PRECISION);
        }

        [Fact]
        public void RayIntersectsDiskCircumference()
        {
            var ray = new Ray(
                new Point3(0), new Vector3(0.5F, 1, 1 - (float)Math.Sqrt(3 - Consts.EPS) / 2)
            );
            var distance = (float)Math.Sqrt(3 - Math.Sqrt(3));
            AssertIntersection(disk, ray, distance);
        }

        [Fact]
        public void RayIntersectsDiskSurface()
        {
            var ray = new Ray(new Point3(0), new Vector3(1));
            var distance = (float)Math.Sqrt(3);
            AssertIntersection(disk, ray, distance);
        }
    }
}
