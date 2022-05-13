namespace Core.Scenario
{
    using Core.Consumer;
    using RayTracer.Scene;
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
                20, 20, 30,
                new Vector3(0, 0, 0),
                new Vector3(0, 0, -1)
            );

            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(0, 0, -1))
            };

            var sphere = new Sphere(new Vector3(-0.2f, 0.2f, -3), 0.5f);
            scene.AddObject(sphere);

            // var disk = new Disk(new Vector3(0, 1, -1), new Vector3(0, 0, -2), 0.5f);
            // scene.AddObject(disk);

            var triangle = new Triangle(
                new Vector3(-0.5f, 0, -1),
                new Vector3(1, -0.2f, -1),
                new Vector3(1, 0.2f, -1)
            );
            scene.AddObject(triangle);

            var tracer = new BasicTracer();
            var adapter = new ConsoleAdapter();
            var renderer = new BasicRenderer<double?>(tracer, adapter);

            var image = renderer.Render(scene);
            var consumer = new ConsoleConsumer();
            consumer.Consume(image, null);
        }
    }
}