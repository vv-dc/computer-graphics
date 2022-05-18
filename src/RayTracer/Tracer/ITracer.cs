namespace RayTracer.Tracer
{
    using RayTracingLib;
    using RayTracingLib.Traceable;

    public interface ITracer
    {
        HitResult? Trace(Ray ray);
        void Init(List<ITraceable> sceneObjects);
    }
}