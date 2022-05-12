namespace RayTracer.Renderer
{
    using RayTracingLib;
    using RayTracer.Scene;

    public interface IRenderer<PixelType>
    {
        Image<PixelType> Render(Scene scene);
    }
}
