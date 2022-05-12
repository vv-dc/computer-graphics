namespace RayTracingLib.Traceable
{
    public interface ITraceable
    {
        bool Intersect(Ray ray, out HitResult? hitResult);
    }
}