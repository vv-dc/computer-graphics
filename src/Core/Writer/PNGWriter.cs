namespace Core.Writer
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    public class PNGWriter : IWriter<RayTracingLib.Color>
    {
        public void Write(RayTracer.Image<RayTracingLib.Color> inImage, string target)
        {
            var outImage = new Image<Rgb24>(inImage.Width, inImage.Height);

            for (int y = 0; y < inImage.Height; ++y)
            {
                for (int x = 0; x < inImage.Width; ++x)
                {
                    var color = inImage[y, x].Normalize();
                    outImage[x, y] = new Rgb24((byte)color.r, (byte)color.g, (byte)color.b);
                }
            }

            outImage.SaveAsPng(target);
        }
    }
}
