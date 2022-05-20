namespace Core.Scenario
{
    using Core.Consumer;
    using RayTracer;
    using RayTracer.Renderer;
    using RayTracer.Tracer;
    using RayTracer.Adapter;
    using RayTracingLib;
    using RayTracingLib.Traceable;
    using RayTracingLib.Numeric;
    using RayTracingLib.Light;

    public class FigureScenario : IScenario
    {
        public void Run()
        {
            var camera = new Camera(
                95, 35, 60,
                new Point3(0, 0, 0),
                new Vector3(0, 0, -1)
            );

            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(1, -1, -1))
            };

            // var sphere = new Sphere(new Point3(0, 0, -2.5f), 1f);
            // sphere.Transform(
            //     Matrix4x4.CreateRotationY(Consts.DegToRad * 35)
            //     * Matrix4x4.CreateScale(0.5f, sphere.Center));
            // scene.AddObject(sphere);

            // var sphere = new Sphere(new Point3(0, 0, -1.25f), 3f);
            // scene.AddObject(sphere);

            var cylinder = new Cylinder(new Point3(0, 0, -2), new Point3(0, 0, -4), 1);
            cylinder.Transform(Matrix4x4.CreateRotationY(
                Consts.DegToRad * -135,
                cylinder.Point1 + (cylinder.Point2 - cylinder.Point1) * 0.5f
            ));
            scene.AddObject(cylinder);

            // var cylinder = new Cylinder(new Point3(0, -1, -3), new Point3(0, 1, -5), 1);
            // scene.AddObject(cylinder);

            // var plane = new Plane(new Vector3(1, 0, 0), new Point3(0, 0, -3));
            // plane.Transform(
            //     // Matrix4x4.CreateRotationX(Consts.DegToRad * 15, plane.Point)
            //     Matrix4x4.CreateRotationY(Consts.DegToRad * 15, plane.Point));
            // scene.AddObject(plane);

            // var plane = new Plane(new Vector3(1, -0.5f, -0.5f), new Point3(0, 0, -3));
            // scene.AddObject(plane);

            // var disk = new Disk(new Vector3(0, -1, -0.25f), new Point3(0, 0, -4), 2f);
            // scene.AddObject(disk);

            // var triangle = new Triangle(
            //     new Point3(-4f, 0, -2.5f),
            //     new Point3(1, -0.5f, -1.25f),
            //     new Point3(8, 0.25f, -8)
            // );
            // triangle.Transform(Matrix4x4.CreateRotationX(Consts.DegToRad * -45));
            // scene.AddObject(triangle);

            var tracer = new BasicTracer();
            // var adapter = new ConsoleAdapter();
            var shadowTracer = new FirstHitTracer();
            var adapter = new ConsoleShadowAdapter(shadowTracer);
            var renderer = new BasicRenderer<Intensity>(tracer, adapter);

            var image = renderer.Render(scene);
            var consumer = new ConsoleConsumer();
            consumer.Consume(image, null);
        }
    }
}
