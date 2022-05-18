namespace RayTracer.Tests.Renderer
{
    using System;
    using System.Collections.Generic;

    using Xunit;

    using RayTracingLib;
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

        private class MatrixComparator : IEqualityComparer<Intensity[,]>
        {
            public bool Equals(Intensity[,] left, Intensity[,] right)
            {
                if (
                    left.Length != right.Length || left.GetLength(1) != right.GetLength(1)
                ) return false;

                for (int i = 0; i < left.GetLength(0); ++i)
                {
                    for (int j = 0; j < left.GetLength(1); ++j)
                        if (!Compare(left[i, j], right[i, j])) return false;
                }
                return true;
            }

            public int GetHashCode(Intensity[,] value) => value.GetHashCode();

            private bool Compare(float left, float right)
            {
                return Math.Abs(left - right) < EPS;
            }
        }

        [Fact]
        public void EmptyImageConsoleRender()
        {
            var camera = new Camera(2, 2, 30, new Point3(0), new Vector3(0, 0, -1));
            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(0, 0, -1))
            };
            var tracer = new BasicTracer();
            var adapter = new ConsoleAdapter();
            var renderer = new BasicRenderer<Intensity>(tracer, adapter);

            // scene has no objects, so image should be empty
            var image = renderer.Render(scene);
            Assert.NotNull(image);

            var actualPixels = image.AsMatrix();
            var bg = Intensity.Background;
            var expectedPixels = new Intensity[,] { { bg, bg }, { bg, bg } };
            Assert.Equal(expectedPixels, actualPixels, new MatrixComparator());
        }

        [Fact]
        public void SphereImageConsoleRender()
        {
            var camera = new Camera(4, 4, 30, new Point3(0), new Vector3(0, 0, -1));
            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(0, 0, -1))
            };
            var tracer = new BasicTracer();
            var adapter = new ConsoleAdapter();
            var renderer = new BasicRenderer<Intensity>(tracer, adapter);

            var sphere = new Sphere(new Point3(-0.2f, 0.2f, -3), 0.5f);
            scene.AddObject(sphere);

            var image = renderer.Render(scene);
            Assert.NotNull(image);

            var actualPixels = image.AsMatrix();
            var bg = Intensity.Background;
            var expectedPixels = new Intensity[,] {
                { bg,     0.754f, bg,     bg },
                { 0.754f, 0.995f, 0.650f, bg },
                { bg,     0.650f, bg,     bg },
                { bg,     bg,     bg,     bg }
            };
            Assert.Equal(expectedPixels, actualPixels, new MatrixComparator());
        }
    }
}
