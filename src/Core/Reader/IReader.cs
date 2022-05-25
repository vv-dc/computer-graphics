namespace Core.Reader
{
    using RayTracer;

    public interface IReader
    {
        IMesh Read(Scene scene, string source);
    }
}
