namespace RayTracer.Tracer
{
    using RayTracingLib;

    public interface ITracer
    {
        bool Trace(Ray ray, out HitResult? hitResult);
        void Init(List<RenderableObject> sceneObjects);
    }
}
