namespace Core.Reader
{
    using RayTracer;

    public interface IReader
    {
        void Read(Scene scene, string source);
    }
}
