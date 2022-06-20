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
    using RayTracingLib.Traceable.KdTree;

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
                lights = new List<Light> {
                    new DirectionalLight(new Vector3(1, -1, -1), Color.White),
                    // new DirectionalLight(new Vector3(0, -1, 0), new Color(1, 1, 1)),
                    // new PointLight(new Point3(0f, -0.25f, 0f), Color.White),
                    // new PointLight(new Point3(0, 0.5f, 0.4f), Color.White),
                    // new PointLight(new Point3(0, 0.3f, 0.3f), Color.White),
                    new PointLight(new Point3(-0.3f, 0.25f, 0.3f), Color.White, 3f),
                    // new EnvironmentalLight(Color.Mirror, 1f),
                    new EnvironmentalLight(Color.Mirror, 0.5f)
                }
            };

            var objParserProvider = new OBJParserProvider();
            var objReader = new OBJReader(objParserProvider);
            IMesh? mesh = null;

            Timer.LogTime(() => mesh = objReader.Read(scene, source!), "Read");

            mesh!.Transform(
            // Matrix4x4.CreateTranslation(-16f, -9f, -35f) *
            // Matrix4x4.CreateTranslation(0, -1f, -2.5f) *
            Matrix4x4.CreateRotationY(-45 * Consts.DegToRad) *
            Matrix4x4.CreateRotationX(-90 * Consts.DegToRad)
            // Matrix4x4.CreateRotationX(-90 * Consts.DegToRad)
            // Matrix4x4.CreateScale(0.5f)
            );

            var nodeSplitter = new SAHSplitter();
            var tree = new KdTree(mesh.GetTraceables(), nodeSplitter, maxPrims: 12, maxDepth: 24);
            var sceneObject = new RenderableObject(tree, new MirrorMaterial(
            // Color.White
            ));
            scene.AddObject(sceneObject);

            var cowSphere = new Sphere(new Point3(-0.1f, 0.6f, -0.3f), 0.2f);
            scene.AddObject(new RenderableObject(cowSphere, new LambertianMaterial(
                Color.Red
            )));

            // var smallSphere = new Sphere(new Point3(-0.3f, 0.25f, 0.3f), 0.01f);
            // scene.AddObject(new RenderableObject(smallSphere, new LambertianMaterial(
            //     Color.White
            // )));

            // var sphere1 = new Sphere(new Point3(0.25f, 0, 0.15f), 0.2f);
            // scene.AddObject(new RenderableObject(sphere1, new LambertianMaterial(
            //     Color.Red
            // )));

            // var sphere2 = new Sphere(new Point3(0.25f, 0.31f, 0.15f), 0.1f);
            // scene.AddObject(new RenderableObject(sphere2, new MirrorMaterial(
            // // Color.Red
            // )));

            // var sphere3 = new Sphere(new Point3(-0.3f, 0.2f, -0.3f), 0.3f);
            // scene.AddObject(new RenderableObject(sphere3, new MirrorMaterial(

            // )));

            // var sphere5 = new Sphere(new Point3(-0.4f, 0, 0.3f), 0.2f);
            // scene.AddObject(new RenderableObject(sphere5, new LambertianMaterial(
            //     Color.Blue
            // )));

            // var centerSphere = new Sphere(new Point3(0, 0, 0), 0.2f);
            // scene.AddObject(new RenderableObject(centerSphere, new LambertianMaterial(
            //     Color.Red
            // )));

            var plane = new Plane(new Vector3(0, -1, 0), new Point3(0, 0, 0));
            scene.AddObject(new RenderableObject(plane, new LambertianMaterial(
                Color.Green
            )));
            plane.Transform(
                Matrix4x4.CreateTranslation(0, -0.3f, 0)
            );

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
