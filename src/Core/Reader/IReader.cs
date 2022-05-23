namespace Core.Reader
{
    using RayTracer;

    public interface IReader
    {
        Mesh Read(Scene scene, string source);
    }
}
