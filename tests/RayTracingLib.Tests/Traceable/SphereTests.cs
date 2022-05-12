namespace RayTracingLib.Tests.Traceable
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    using RayTracingLib.Traceable;
    using RayTracingLib.Numeric;

    public class SphereTests
    {
        private const int PRECISION = 6;
        private readonly Sphere sphere;

        public SphereTests()
        {
            sphere = new Sphere(new Vector3(1), 3);
        }

        public static IEnumerable<object[]> GetNoIntersectionData()
        {
            yield return new object[] {
                new Ray(new Vector3(5), new Vector3(1)) }; // the sphere is in opposite direction, roots are negative

            yield return new object[] {
                new Ray(new Vector3(5), new Vector3(0, 0, 1)) }; // the line the ray lies on does not intersect the sphere
        }

        [Theory]
        [MemberData(nameof(GetNoIntersectionData))]
        public void NoIntersectionTest(Ray ray)
        {
            Assert.False(sphere.Intersect(ray, out var hitResult));
            Assert.Null(hitResult);
        }

        public static IEnumerable<object[]> GetIntersectionData()
        {
            yield return new object[] {
                new Ray(new Vector3(0, 0, -2), new Vector3(1, 1, 0)), Math.Sqrt(2) }; // 1 intersection point

            yield return new object[] {
                new Ray(new Vector3(-3), new Vector3(1)), 4 * Math.Sqrt(3) - 3 }; // 2 intersection points

            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(1)), 3 + Math.Sqrt(3) }; // the ray origin is inside the sphere

            yield return new object[] {
                new Ray(new Vector3(1, 1, 4), new Vector3(0, 0, -1)), 0 }; // the ray origin lies on sphere, the ray looks inside the sphere

            yield return new object[] {
                new Ray(new Vector3(1, 1, 4), new Vector3(1)), 0 }; // the ray origin lies on sphere, the ray looks outside of the sphere
        }

        [Theory]
        [MemberData(nameof(GetIntersectionData))]
        public void IntersectionTest(Ray ray, float expected)
        {
            Assert.True(sphere.Intersect(ray, out var hitResult));
            Assert.Equal(expected, hitResult!.distance, PRECISION);
        }
    }
}