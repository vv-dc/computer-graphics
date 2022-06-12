namespace Core.Scenario
{
    using Common;
    using Common.Numeric;
    using Core.Writer;
    using RayTracer;
    using RayTracer.Adapter;
    using RayTracer.Renderer;
    using RayTracer.Tracer;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

    public class ConsoleScenario : IScenario
    {
        public void Run(string? source, string? output, int width, int height)
        {
            var camera = new Camera(
                width, height, 60,
                new Point3(0, 0, 0),
                new Vector3(0, 0, -1)
            );

            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(1, -1, -1))
            };

            // var sphere = new Sphere(new Point3(0, 0, -2.5f), 1f);
            // // sphere.Transform(
            // //     Matrix4x4.CreateRotationY(Consts.DegToRad * 35)
            // //     * Matrix4x4.CreateScale(0.5f, sphere.Center));
            // scene.AddObject(sphere);

            // var sphere = new Sphere(new Point3(0, 0, -1.25f), 3f);
            // scene.AddObject(sphere);

            // var cylinder = new Cylinder(new Point3(0, 0, -2), new Point3(0, 0, -4), 1);
            // cylinder.Transform(Matrix4x4.CreateRotationY(
            //     Consts.DegToRad * -135,
            //     cylinder.Point1 + (cylinder.Point2 - cylinder.Point1) * 0.5f
            // ));
            // scene.AddObject(cylinder);

            // var cylinder = new Cylinder(new Point3(0, -1, -3), new Point3(0, 1, -5), 1);
            // scene.AddObject(cylinder);

            // var plane = new Plane(new Vector3(1, 0, 0), new Point3(0, 0, -3));
            // plane.Transform(
            // Matrix4x4.CreateRotationX(Consts.DegToRad * 15, plane.Point));
            // Matrix4x4.CreateRotationY(Consts.DegToRad * 15, plane.Point));
            // scene.AddObject(plane);

            // var plane = new Plane(new Vector3(1, -0.5f, -0.5f), new Point3(0, 0, -3));
            // scene.AddObject(plane);

            // var disk = new Disk(new Vector3(0, -1, -0.25f), new Point3(0, 0, -4), 2f);
            // disk.Transform(
            //     Matrix4x4.CreateTranslation(0, 1f, -2)
            // );
            // scene.AddObject(disk);

            // var triangle = new Triangle(
            //     new Point3(-4f, 0, -2.5f),
            //     new Point3(1, -0.5f, -1.25f),
            //     new Point3(8, 0.25f, -8)
            // );
            // triangle.SetDefaultNormals();
            // triangle.Transform(Matrix4x4.CreateRotationX(Consts.DegToRad * -45));
            // scene.AddObject(triangle);

            var tracer = new BasicTracer();
            var shadowTracer = new FirstHitTracer();

            var intensityAdapter = new IntensityShadowAdapter(shadowTracer);
            var adapter = new ColorAdapter(intensityAdapter);
            var renderer = new BasicRenderer<Color>(tracer, adapter);
            // var renderer = new BasicRenderer<Intensity>(tracer, intensityAdapter);

            var image = renderer.Render(scene);
            var writer = new PNGWriter();
            writer.Write(image, output!);
        }
    }
}
