namespace Core.Writer
{
    using System;
    using System.IO;
    using System.Text;

    using RayTracer;

    public class PPMWriter : IWriter<Color>
    {
        public void Write(Image<Color> image, string target)
        {
            string imageBuffer = ComposeImage(image);
            using (StreamWriter stream = BuildStreamWriter(target))
            {
                stream.Write(imageBuffer);
            }
        }

        private StreamWriter BuildStreamWriter(String path)
        {
            return new StreamWriter(path);
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
                    string triplet = ComposeTriplet(image[y, x]);
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
