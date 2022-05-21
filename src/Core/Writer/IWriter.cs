namespace Core.Writer
{
    using RayTracer;

    public interface IWriter<PixelType>
    {
        void Consume(Image<PixelType> image, string? target);
    }
}
