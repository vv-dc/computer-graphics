namespace Core.Scenario
{
    using Common;
    using Common.Timer;
    using Common.Numeric;
    using Core.Reader;
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
                int.Parse(args[2]),
                int.Parse(args[3]),
                60,
                new Point3(0, 0, 1.15f),
                new Vector3(0, 0, -1)
            );

            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(1, -1, -1))
            };

            var objReader = new OBJReader();
            Mesh? mesh = null;

            Timer.LogTime(() =>
            {
                mesh = objReader.Read(scene, args[0]);
            }, "Read");

            mesh!.Transform(
                Matrix4x4.CreateRotationY(-45 * Consts.DegToRad) *
                Matrix4x4.CreateRotationX(-90 * Consts.DegToRad)
            );
            scene.SetObjects(mesh.GetTraceables());

            var tracer = new BasicTracer();
            var shadowTracer = new FirstHitTracer();

            var intensityAdapter = new IntensityShadowAdapter(shadowTracer);
            var adapter = new ColorAdapter(intensityAdapter);
            var renderer = new ParallelRenderer<Color>(tracer, adapter);

            Image<Color>? image = null;
            Timer.LogTime(() =>
            {
                image = renderer.Render(scene);
            }, "Render");

            var consumer = new PNGWriter();
            Timer.LogTime(() =>
            {
                consumer.Write(image!, args[1]);
            }, "Write");
        }
    }
}
