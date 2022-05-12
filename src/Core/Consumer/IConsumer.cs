namespace Core.Consumer
{

    using RayTracingLib;
    public interface IConsumer<PixelType>
    {
        void Consume(Image<PixelType> image, string? target);
    }
}
