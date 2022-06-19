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
    using RayTracingLib;
    using RayTracingLib.Material;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

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
                Light = new DirectionalLight(new Vector3(1, -1, -1), new Color(1, 0, 0))
            };

            var objParserProvider = new OBJParserProvider();
            var objReader = new OBJReader(objParserProvider);
            IMesh? mesh = null;

            Timer.LogTime(() => mesh = objReader.Read(scene, source!), "Read");

            mesh!.Transform(
            Matrix4x4.CreateTranslation(-16f, -9f, -35f) *
            // Matrix4x4.CreateTranslation(0, -1f, -2.5f) *
            Matrix4x4.CreateRotationY(45 * Consts.DegToRad)
            // Matrix4x4.CreateRotationX(-90 * Consts.DegToRad)
            // Matrix4x4.CreateScale(0.5f)
            );

            // var nodeSplitter = new SAHSplitter();
            // var tree = new KdTree(mesh.GetTraceables(), nodeSplitter, maxDepth: 20, maxPrims: 16);
            // var sceneObject = new RenderableObject(tree, new MirrorMaterial(
            //     // Color.Green
            // ));
            // scene.AddObject(sceneObject);
            // scene.AddObject(tree.Bounds);
            // scene.SetObjects(mesh.GetTraceables());

            // var sphere = new Sphere(new Point3(-0.3f, 0, 0.2f), 0.3f);
            // scene.AddObject(new RenderableObject(sphere, new LambertianMaterial(Color.Black)));

            var sphere1 = new Sphere(new Point3(0.25f, 0, 0.15f), 0.2f);
            scene.AddObject(new RenderableObject(sphere1, new LambertianMaterial(
                Color.White
            )));

            var sphere2 = new Sphere(new Point3(0.25f, 0.31f, 0.15f), 0.1f);
            scene.AddObject(new RenderableObject(sphere2, new MirrorMaterial(
            // Color.Red
            )));

            var sphere3 = new Sphere(new Point3(-0.3f, 0.2f, -0.3f), 0.3f);
            scene.AddObject(new RenderableObject(sphere3, new MirrorMaterial(

            )));

            var sphere4 = new Sphere(new Point3(0, -10.2f, 0f), 10f);
            scene.AddObject(new RenderableObject(sphere4, new MirrorMaterial(
                // Color.White
                )
            ));

            var sphere5 = new Sphere(new Point3(-0.4f, 0, 0.3f), 0.2f);
            scene.AddObject(new RenderableObject(sphere5, new LambertianMaterial(
                Color.Blue
            )));

            var tracer = new BasicTracer();
            var shadowTracer = new FirstHitTracer();

            var intensityAdapter = new IntensityShadowAdapter(shadowTracer);
            var adapter = new MaterialAdapter(tracer, shadowTracer);
            // var adapter = new ColorAdapter(intensityAdapter);
            var renderer = new ParallelRenderer<Color>(tracer, adapter);

            Image<Color>? image = null;
            Timer.LogTime(() => image = renderer.Render(scene), "Render");

            var consumer = new PNGWriter();
            Timer.LogTime(() => consumer.Write(image!, output!), "Write");
        }
    }
}
