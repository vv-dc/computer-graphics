namespace RayTracingLib
{
    using RayTracingLib.Traceable;

    public interface ITreeTraceable : ITraceable
    {
        AABB GetAABB();
    }
}
