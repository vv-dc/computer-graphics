namespace RayTracer.Tracer
{
    using RayTracingLib;
    using RayTracer.Scene;

    public interface ITracer
    {
        bool Trace(Ray ray, out HitResult? hitResult);
        void Init(Scene scene);
    }
}