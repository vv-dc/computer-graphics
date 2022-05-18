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
        public void Run(string[] args)
        {
            var camera = new Camera(
                90, 90, 60,
                new Point3(0, 0, 0),
                new Vector3(0, 0, -1)
            );

            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(1, -1, -1))
            };

            var sphere = new Sphere(new Point3(0, 0, -2.5f), 1f);
            scene.AddObject(sphere);

            // var sphere = new Sphere(new Point3(0, 0, -1.25f), 0.5f);
            // scene.AddObject(sphere);

            // var cylinder = new Cylinder(new Point3(0, 1, -3), new Point3(0, 2, -3.5f), 0.5f);
            // scene.AddObject(cylinder);

            // var cylinder = new Cylinder(new Point3(0, -1, -3), new Point3(0, 0, -5), 0.5f);
            // scene.AddObject(cylinder);

            // var plane = new Plane(new Vector3(0, -1, -1E-6F), new Point3(0, 0, -3));
            // scene.AddObject(plane);

            // var disk = new Disk(new Vector3(0, -1, -0.25f), new Point3(0, 0, -4), 2f);
            // scene.AddObject(disk);

            var triangle = new Triangle(
                new Point3(-4f, 0, -2.5f),
                new Point3(1, -0.5f, -1.25f),
                new Point3(8, 0.25f, -8)
            );
            scene.AddObject(triangle);

            var tracer = new BasicTracer();
            var adapter = new ConsoleAdapter();
            var renderer = new BasicRenderer<Intensity>(tracer, adapter);

            var image = renderer.Render(scene);
            var consumer = new ConsoleConsumer();
            consumer.Consume(image, null);
        }
    }
}
