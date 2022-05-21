namespace Core.Writer
{
    using RayTracer;

    public interface IWriter<PixelType>
    {
        void Write(Image<PixelType> image, string target);
    }
}
