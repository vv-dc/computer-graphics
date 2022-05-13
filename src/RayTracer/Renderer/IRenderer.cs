namespace RayTracer.Renderer
{
    using RayTracingLib;
    using RayTracer;

    public interface IRenderer<PixelType>
    {
        Image<PixelType> Render(Scene scene);
    }
}
