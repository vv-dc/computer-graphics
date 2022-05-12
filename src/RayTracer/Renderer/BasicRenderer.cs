namespace RayTracer.Renderer
{
    using RayTracingLib;
    using RayTracer.Scene;
    using RayTracer.Tracer;
    using RayTracer.Adapter;

    public class BasicRenderer<PixelType> : IRenderer<PixelType>
    {
        private readonly ITracer tracer;

        private readonly IAdapter<PixelType> adapter;

        public BasicRenderer(ITracer tracer, IAdapter<PixelType> adapter)
        {
            this.tracer = tracer;
            this.adapter = adapter;
        }

        public Image<PixelType> Render(Scene scene)
        {
            tracer.Init(scene);
            Camera camera = scene.camera;

            int width = camera.width;
            int height = camera.height;
            var image = new Image<PixelType>(width, height);

            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    Ray newRay = camera.CastRay(x, y);
                    if (tracer.Trace(newRay, out HitResult? hitResult))
                    {
                        image[y, x] = adapter.Adapt(scene.Light, hitResult!);
                    }
                }
            }

            return image;
        }
    }
}
