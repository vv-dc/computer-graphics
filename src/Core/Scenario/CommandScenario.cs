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
    using RayTracingLib.Traceable;
    using RayTracingLib.Traceable.KdTree;
    using RayTracingLib.Light;

    public class CommandScenario : IScenario
    {
        public void Run(string? source, string? output, int width, int height)
        {
            var camera = new Camera(
                width, height, 60,
                new Point3(0, 0, 1.15f),
                new Vector3(0, 0, -1)
            );

            var scene = new Scene(camera)
            {
                Light = new DirectionalLight(new Vector3(1, -1, -1))
            };

            var objParserProvider = new OBJParserProvider();
            var objReader = new OBJReader(objParserProvider);
            IMesh? mesh = null;

            Timer.LogTime(() => mesh = objReader.Read(scene, source!), "Read");

            mesh!.Transform(
            Matrix4x4.CreateTranslation(0, -8f, -35f) *
            // Matrix4x4.CreateTranslation(0, -1f, -2.5f) *
            Matrix4x4.CreateRotationY(45 * Consts.DegToRad)
            // Matrix4x4.CreateRotationX(-90 * Consts.DegToRad)
            // Matrix4x4.CreateScale(0.5f)
            );

            var nodeSplitter = new SAHSplitter();
            var tree = new KdTree(mesh.GetTraceables(), nodeSplitter, maxDepth: 20, maxPrims: 16);
            scene.AddObject(tree);
            // scene.AddObject(tree.Bounds);
            // scene.SetObjects(mesh.GetTraceables());

            var tracer = new BasicTracer();
            var shadowTracer = new FirstHitTracer();

            var intensityAdapter = new IntensityShadowAdapter(shadowTracer);
            var adapter = new ColorAdapter(intensityAdapter);
            var renderer = new ParallelRenderer<Color>(tracer, adapter);

            Image<Color>? image = null;
            Timer.LogTime(() => image = renderer.Render(scene), "Render");

            var consumer = new PNGWriter();
            Timer.LogTime(() => consumer.Write(image!, output!), "Write");
        }
    }
}
