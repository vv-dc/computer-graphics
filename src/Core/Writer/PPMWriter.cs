namespace Core.Writer
{
    using System.IO;
    using System.Text;

    using RayTracer;
    using RayTracingLib;

    public class PPMWriter : IWriter<RayTracingLib.Color>
    {
        public void Write(Image<RayTracingLib.Color> image, string target)
        {
            string imageBuffer = ComposeImage(image);
            using (StreamWriter stream = BuildStreamWriter(target))
            {
                stream.Write(imageBuffer);
            }
        }

        private StreamWriter BuildStreamWriter(string path)
        {
            return new StreamWriter(path, false, Encoding.ASCII);
        }

        private string ComposeImage(Image<Color> image)
        {
            string header = ComposeHeader(image.Width, image.Height);
            var body = new StringBuilder();
            body.AppendLine(header);

            for (int y = 0; y < image.Height; ++y)
            {
                for (int x = 0; x < image.Width; ++x)
                {
                    string triplet = ComposeTriplet(image[y, x].Normalize());
                    body.AppendLine(triplet);
                }
            }

            return body.ToString();
        }

        private string ComposeTriplet(Color pixel)
        {
            return $"{pixel.r} {pixel.g} {pixel.b}";
        }

        private string ComposeHeader(int width, int height)
        {
            return $"P3\n{width} {height}\n255";
        }
    }
}
