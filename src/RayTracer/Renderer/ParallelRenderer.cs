namespace RayTracer.Renderer
{
    using System.Threading;

    using Common.ProgressBar;
    using RayTracer.Adapter;
    using RayTracer.Tracer;
    using RayTracingLib;

    public class ParallelRenderer<PixelType> : IRenderer<PixelType>
    {
        private static Random random = new();
        private readonly ITracer tracer;
        private readonly IAdapter<PixelType> adapter;
        private int numTasks;

        public ParallelRenderer(ITracer tracer, IAdapter<PixelType> adapter, int numTasks)
        {
            this.tracer = tracer;
            this.adapter = adapter;
            this.numTasks = numTasks;
        }

        public ParallelRenderer(ITracer tracer, IAdapter<PixelType> adapter)
        : this(tracer, adapter, Environment.ProcessorCount) { }

        public Image<PixelType> Render(Scene scene)
        {
            tracer.Init(scene.objects);
            adapter.Init(scene.objects);

            Camera camera = scene.camera;
            var image = new Image<PixelType>(camera.width, camera.height);

            return RenderParallel(scene, image);
        }

        private Image<PixelType> RenderParallel(Scene scene, Image<PixelType> image)
        {
            var coords = GetPixelCoords(image.Width, image.Height).OrderBy(element => random.Next()).ToList();
            var size = image.Width * image.Height;
            var chunkSize = (int)MathF.Ceiling(size / (float)numTasks);

            var progressBar = new EtaProgressBar(size, label: "Render");
            using (var countdownEvent = new CountdownEvent(numTasks))
            {
                for (int idx = 0; idx < numTasks; ++idx)
                {
                    int start = idx * chunkSize;
                    var chunk = coords.GetRange(start, Math.Min(chunkSize, size - start));

                    ThreadPool.QueueUserWorkItem((_) =>
                    {
                        RenderPixels(scene, image, progressBar, chunk);
                        countdownEvent.Signal();
                    });
                }
                countdownEvent.Wait();
            }
            return image;
        }

        private void RenderPixels(
            Scene scene,
            Image<PixelType> image,
            EtaProgressBar progressBar,
            List<(int, int)> coords
        )
        {
            Camera camera = scene.camera;
            foreach (var (y, x) in coords)
            {
                Ray ray = camera.CastRay(x, y);
                tracer.Trace(ray, out var hitResult);
                image[y, x] = adapter.Adapt(scene.Light, hitResult);
                progressBar.Next();
            }
        }

        private List<(int, int)> GetPixelCoords(int width, int height)
        {
            var coords = new List<(int, int)>(width * height);

            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x)
                    coords.Add((y, x));

            return coords;
        }
    }
}
