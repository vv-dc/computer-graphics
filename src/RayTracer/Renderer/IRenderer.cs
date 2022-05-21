namespace RayTracer.Renderer
{
    using RayTracer;

    public interface IRenderer<PixelType>
    {
        Image<PixelType> Render(Scene scene);
    }
}
