namespace RayTracer.Tests.Renderer
{
    using System;
    using System.Collections.Generic;

    using Xunit;

    using RayTracingLib.Numeric;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;
    using RayTracer;
    using RayTracer.Tracer;
    using RayTracer.Renderer;
    using RayTracer.Adapter;

    public class BasicRendererTests
    {
        public const float EPS = 1E-3F;

        private class MatrixComparator : IEqualityComparer<double?[,]>
        {
            public bool Equals(double?[,] a, double?[,] b)
            {
                if (a.Length != b.Length || a.GetLength(1) != b.GetLength(1)) return false;
                for (int i = 0; i < a.GetLength(0); ++i)
                {
                    for (int j = 0; j < a.GetLength(1); ++j)
                        if (!Compare(a[i, j], b[i, j])) return false;
                }
                return true;
            }

            public int GetHashCode(double?[,] value) => value.GetHashCode();

            private bool Compare(double? a, double? b)
            {
                if (a == b) return true; // if null or completely same
                if (a is null || b is null) return false; // only one of values is null
                return Math.Abs((double)(a - b)) < EPS;
            }
        }

        [Fact]
        public void EmptyImageConsoleRender()
        {
            var camera = new Camera(2, 2, 30, new Vector3(0, 0, 0), new Vector3(0, 0, -1));
            var scene = new Scene(camera) { Light = new DirectionalLight(new Vector3(0, 0, -1)) };
            var tracer = new BasicTracer();
            var adapter = new ConsoleAdapter();
            var renderer = new BasicRenderer<double?>(tracer, adapter);

            // scene has no objects, so image should be empty
            var image = renderer.Render(scene);
            Assert.NotNull(image);

            var actualPixels = image.AsMatrix();
            var expectedPixels = new double?[,] {
                { null, null },
                { null, null }
            };
            Assert.Equal(expectedPixels, actualPixels, new MatrixComparator());
        }

        [Fact]
        public void SphereImageConsoleRender()
        {
            var camera = new Camera(4, 4, 30, new Vector3(0, 0, 0), new Vector3(0, 0, -1));
            var scene = new Scene(camera) { Light = new DirectionalLight(new Vector3(0, 0, -1)) };
            var tracer = new BasicTracer();
            var adapter = new ConsoleAdapter();
            var renderer = new BasicRenderer<double?>(tracer, adapter);

            var sphere = new Sphere(new Vector3(-0.2f, 0.2f, -3), 0.5f);
            scene.AddObject(sphere);

            var image = renderer.Render(scene);
            Assert.NotNull(image);

            var actualPixels = image.AsMatrix();
            var expectedPixels = new double?[,] {
                { null,   0.754f, null, null },
                { 0.754f, 0.995f, 0.650f, null },
                { null,   0.650f, null, null },
                { null,    null, null, null }
            };
            Assert.Equal(expectedPixels, actualPixels, new MatrixComparator());
        }
    }
}
