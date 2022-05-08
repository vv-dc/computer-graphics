namespace RayTracingLib.Tests.Traceable
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    using RayTracingLib.Traceable;
    using RayTracingLib.Numeric;

    public class TriangleTests
    {
        private const int PRECISION = 6;
        private readonly Triangle triangle;

        public TriangleTests()
        {
            triangle = new Triangle(
                new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        }

        public static IEnumerable<object[]> GetNoIntersectionData()
        {
            yield return new object[] {
                new Ray(new Vector3(2), new Vector3(1)) }; // the triangle is in opposite direction

            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(1, -1E-3f, 1)) }; // the ray passes near the triangle edge

            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(-1, 1, 1)) };
        }

        [Theory]
        [MemberData(nameof(GetNoIntersectionData))]
        public void NoIntersectionTest(Ray ray)
        {
            Assert.False(triangle.Intersect(ray, out var distance));
            Assert.Equal(0, distance);
        }

        public static IEnumerable<object[]> GetIntersectionData()
        {
            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(1, 1, 0)), 1 / Math.Sqrt(2) }; // the ray intersects the triangle edge

            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(1, 0, 0)), 1 }; // the ray intersects the triangle vertex

            yield return new object[] {
                new Ray(new Vector3(0), new Vector3(1)), 1 / Math.Sqrt(3) }; // the ray intersects the triangle vertex
        }

        [Theory]
        [MemberData(nameof(GetIntersectionData))]
        public void IntersectionTest(Ray ray, float expected)
        {
            Assert.True(triangle.Intersect(ray, out var distance));
            Assert.Equal(expected, distance, PRECISION);
        }
    }
}