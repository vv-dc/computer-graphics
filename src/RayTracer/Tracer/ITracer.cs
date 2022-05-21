namespace RayTracer.Tracer
{
    using RayTracingLib;
    using RayTracingLib.Traceable;

    public interface ITracer
    {
        bool Trace(Ray ray, out HitResult? hitResult);
        void Init(List<ITraceable> sceneObjects);
    }
}
