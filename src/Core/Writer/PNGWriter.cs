namespace Core.Writer
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    public class PNGWriter : IWriter<RayTracer.Color>
    {
        public void Write(RayTracer.Image<RayTracer.Color> inImage, string target)
        {
            var outImage = new Image<Rgb24>(inImage.Width, inImage.Height);

            for (int y = 0; y < inImage.Height; ++y)
            {
                for (int x = 0; x < inImage.Width; ++x)
                {
                    var color = inImage[y, x];
                    outImage[x, y] = new Rgb24(color.r, color.g, color.b);
                }
            }

            outImage.SaveAsPng(target);
        }
    }
}
