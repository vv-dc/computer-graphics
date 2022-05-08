namespace RayTracingLib.Tests.Traceable
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    using RayTracingLib.Traceable;
    using RayTracingLib.Numeric;

    public class PlaneTests
    {
        private const int PRECISION = 6;
        private readonly Plane plane;

        public PlaneTests()
        {
            plane = new Plane(new Vector3(1), new Vector3(1));
        }

        public static IEnumerable<object[]> GetNoIntersectionData()
        {
            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(-1)) }; // the plane is in opposite direction

            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(-1, 1, 0)) }; // the ray is parallel to the plane

            yield return new object[] {
                new Ray(new Vector3(0, 0, 3), new Vector3(1, -1, 0)) }; // the ray lies on the plane
        }

        [Theory]
        [MemberData(nameof(GetNoIntersectionData))]
        public void NoIntersectionTest(Ray ray)
        {
            Assert.False(plane.Intersect(ray, out var distance));
            Assert.Equal(0, distance);
        }

        public static IEnumerable<object[]> GetIntersectionData()
        {
            yield return new object[] {
                new Ray(new Vector3(3, 0, 0), new Vector3(1, 0, 1)), 0 }; // the ray origin lies on the plane

            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(1)), Math.Sqrt(3) };

        }

        [Theory]
        [MemberData(nameof(GetIntersectionData))]
        public void IntersectionTest(Ray ray, float expected)
        {
            Assert.True(plane.Intersect(ray, out var distance));
            Assert.Equal(expected, distance, PRECISION);
        }
    }
}