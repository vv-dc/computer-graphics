namespace RayTracer.Renderer
{
    using RayTracingLib;
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
            tracer.Init(scene.objects);
            Camera camera = scene.camera;

            var image = new Image<PixelType>(camera.width, camera.height);
            for (var y = 0; y < image.Height; ++y)
            {
                for (var x = 0; x < image.Width; ++x)
                {
                    Ray ray = camera.CastRay(x, y);
                    tracer.Trace(ray, out var hitResult);
                    image[y, x] = adapter.Adapt(scene.Light, hitResult);
                }
            }
            return image;
        }
    }
}
