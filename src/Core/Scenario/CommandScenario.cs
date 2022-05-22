namespace Core.Scenario
{
    using Common;
    using Common.Numeric;
    using Core.Writer;
    using Core.Reader.OBJReader;
    using RayTracer;
    using RayTracer.Adapter;
    using RayTracer.Renderer;
    using RayTracer.Tracer;
    using RayTracingLib.Light;

    public class CommandScenario : IScenario
    {
        public void Run(string[] args)
        {
            var camera = new Camera(
                300, 300, 60,
                new Point3(0, 0, 1.15f),
                new Vector3(0, 0, -1)
            );

            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(1, -1, -1))
            };

            var objReader = new OBJReader();
            objReader.Read(scene, args[0]);
            Console.WriteLine($"Triangles: {scene.objects.Count}");

            foreach (var sceneObject in scene.objects)
            {
                sceneObject.Transform(
                    Matrix4x4.CreateRotationY(-45 * Consts.DegToRad) *
                    Matrix4x4.CreateRotationX(-90 * Consts.DegToRad)
                // Matrix4x4.CreateRotationZ(-10 * Consts.DegToRad) 
                );
            }

            var tracer = new BasicTracer();
            var shadowTracer = new FirstHitTracer();

            var intensityAdapter = new IntensityShadowAdapter(shadowTracer);
            var adapter = new ColorAdapter(intensityAdapter);
            var renderer = new BasicRenderer<Color>(tracer, adapter);

            var image = renderer.Render(scene);
            Console.WriteLine("Render done");

            var consumer = new PPMWriter();
            consumer.Write(image, args[1]);
        }
    }
}
