namespace RayTracer.Renderer
{
    using System.Threading;

    using Common.ProgressBar;
    using RayTracer.Adapter;
    using RayTracer.Tracer;
    using RayTracingLib;

    public class ParallelRenderer<PixelType> : IRenderer<PixelType>
    {
        private static readonly int THREADS_NUMBER = Environment.ProcessorCount;

        private readonly ITracer tracer;

        private readonly IAdapter<PixelType> adapter;

        public ParallelRenderer(ITracer tracer, IAdapter<PixelType> adapter)
        {
            this.tracer = tracer;
            this.adapter = adapter;
        }

        public Image<PixelType> Render(Scene scene)
        {
            tracer.Init(scene.objects);
            adapter.Init(scene.objects);

            Camera camera = scene.camera;
            var random = new Random();

            var chunkSize = (int)MathF.Ceiling(camera.height * camera.width / THREADS_NUMBER);
            var points = GetAllPoints(camera.width, camera.height)
                .OrderBy(element => random.Next()).ToList(); // shuffle

            return RenderParallel(scene, chunkSize, points);
        }

        private Image<PixelType> RenderParallel(Scene scene, int chunkSize, List<(int, int)> points)
        {
            var image = new Image<PixelType>(scene.camera.width, scene.camera.height);
            var progressBar = new EtaProgressBar(image.Width * image.Height);
            progressBar.StartTimer();

            using (var countdownEvent = new CountdownEvent(THREADS_NUMBER))
            {
                for (int idx = 0; idx < THREADS_NUMBER; ++idx)
                {
                    int start = idx * chunkSize;
                    int chunkSizeBox = Math.Min(chunkSize, points.Count - start);
                    var chunk = points.GetRange(start, chunkSizeBox);

                    ThreadPool.QueueUserWorkItem(
                        (x) =>
                        {
                            RenderPoints(scene, image, progressBar, chunk);
                            countdownEvent.Signal();
                        }
                    );
                }
                countdownEvent.Wait();
            }

            return image;
        }

        private List<(int, int)> GetAllPoints(int width, int height)
        {
            var points = new List<(int, int)>();
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    points.Add((y, x));
                }
            }
            return points;
        }

        private void RenderPoints(
            Scene scene,
            Image<PixelType> image,
            EtaProgressBar progressBar,
            List<(int, int)> points
        )
        {
            Camera camera = scene.camera;
            foreach (var (y, x) in points)
            {
                Ray ray = camera.CastRay(x, y);
                tracer.Trace(ray, out var hitResult);
                image[y, x] = adapter.Adapt(scene.Light, hitResult);
                progressBar.AddAndRefresh(1, "Render");
            }
        }
    }
}
